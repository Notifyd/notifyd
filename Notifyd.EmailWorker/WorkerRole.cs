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
using Notifyd.Core.Contracts;
using Notifyd.Core.Managers;
using Notifyd.Core.Models;
using Notifyd.Core.Operations;

namespace Notifyd.EmailWorker
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "Messages";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient Client;

        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Notifyd.Core.Logs.Logger.LogInfo("Starting processing of messages");
            
            // Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
            Client.OnMessage((receivedMessage) =>
                {
                    Notifyd.Core.Logs.Logger.LogInfo("Receiving Message to Process","Notifyd.EmailWorker");
                    try
                    {
                      
                        // Process the message
                        Notifyd.Core.Logs.Logger.LogInfo("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString(),"Notifyd.EmailWorker");

                        MessageContract order = receivedMessage.GetBody<Notifyd.Core.Contracts.MessageContract>();
                        Notifyd.Core.Logs.Logger.LogInfo(order.PartitionKey + ": " + order.RowKey);

                        // Load Message Object
                       MessageEntity msg = MessageManager.GetMessage(order.PartitionKey, order.RowKey);

                       MailManager manager = new MailManager();

                       manager.Send(msg);

                       msg.Status = "0";
                       msg.CompletedOn = DateTime.Now.ToString();

                        // Mark Message as complete in Queue
                        receivedMessage.Complete();
                    }
                    catch (Exception e)
                    {
                        // Handle any message processing specific exceptions here
                        Notifyd.Core.Logs.Logger.LogError(e.ToString(), "Notifyd.EmailWorker");
                  
                    }
                });

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            Notifyd.Core.Logs.Logger.LogInfo("Starting EmailWorker", "Notifyd.EmailWorker");


            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Create the queue if it does not exist already
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            if (!namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.CreateQueue(QueueName);
            }

            // Initialize the connection to Service Bus Queue
            Client = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            return base.OnStart();
        }

        public override void OnStop()
        {
     
            Notifyd.Core.Logs.Logger.LogInfo("Stopping EmailWorker", "Notifyd.EmailWorker");
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}
