using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Notifyd.Core.Contracts;
using Notifyd.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;
using SendGridMail;
using System.Net.Mail;
using SendGridMail.Transport;

namespace Notifyd.EmailWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "mail";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;
        CloudTableClient _TableClient;
        CloudTable _MsgTable;

        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.WriteLine("Starting processing of messages");

            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            Client.OnMessage((receivedMessage) =>
                {
                    try
                    {
                        // Process the message
                        Trace.WriteLine("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());
                    }
                    catch
                    {
                        // Handle any message processing specific exceptions here
                    }
                });

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            // Create the queue if it does not exist already
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize table storage
            _TableClient = storageAccount.CreateCloudTableClient();
           // tableServiceContext = tableClient.GetDataServiceContext();

           _MsgTable  = _TableClient.GetTableReference("Messages");

           _MsgTable.CreateIfNotExists();

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }

        private void ProcessMessage(BrokeredMessage msg)
        {

            Notifyd.Core.Contracts.MessageContract contract;

            contract = msg.GetBody<MessageContract>();

            // Create a retrieve operation that takes a entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<MessageEntity>(contract.PartitionKey,contract.RowKey);

            // Execute the retrieve operation.
            TableResult retrievedResult = _MsgTable.Execute(retrieveOperation);

            MessageEntity entity = (MessageEntity)retrievedResult.Result;

            Core.Operations.MailManager manager = new Core.Operations.MailManager();

            // Update Message Entity and Save
            // Change the Complete Stamp
            entity.CompletedOn = Convert.ToString(DateTime.Now);

            // Create the InsertOrReplace TableOperation
            TableOperation updateOperation = TableOperation.Replace(entity);
            
            // Execute the operation.
            _MsgTable.Execute(updateOperation);


        }

        private void SendMessage()
        {
            // Create the email object first, then add the properties.
            SendGrid myMessage2 = SendGrid.GetInstance();

            myMessage2.AddTo("byronpate@gmail.com");
            myMessage2.From = new MailAddress("john@example.com", "John Smith");
            myMessage2.Subject = "Testing the SendGrid Library";
            myMessage2.Text = "Hello World!";

            //create an instance of the Web transport mechanism
            var transportInstance = Web.GetInstance(new NetworkCredential("sn_svc", "Dec2011!"));

            //send the mail
            transportInstance.Deliver(myMessage2);
        }
    }
}
