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
                    string sSource;
                    string sLog;

                    sSource = "Application";
                    sLog = "Application";

                    if (!EventLog.SourceExists(sSource))
                        EventLog.CreateEventSource(sSource, sLog);

                    EventLog.WriteEntry(sSource, "Runnning EmailWorker");

                    try
                    {
                        EventLog.WriteEntry(sSource, "Processing EmailWorker");

                        // Process the message
                        Notifyd.Core.Logs.Logger.LogInfo("Processing Service Bus message: " + receivedMessage.SequenceNumber.ToString());

                        MessageContract order = receivedMessage.GetBody<Notifyd.Core.Contracts.MessageContract>();
                        Notifyd.Core.Logs.Logger.LogInfo(order.PartitionKey + ": " + order.RowKey);

                        receivedMessage.Complete();
                    }
                    catch (Exception e)
                    {
                        // Handle any message processing specific exceptions here
                        EventLog.WriteEntry(sSource, e.ToString());
                    }
                });

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            Notifyd.Core.Logs.Logger.LogInfo("Starting EmailWorker");

            string sSource;
            string sLog;
         
            sSource = "Application";
            sLog = "Application";
         
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, "Starting EmailWorker");
 

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
            string sSource;
            string sLog;

            sSource = "Application";
            sLog = "Application";
            
            EventLog.WriteEntry(sSource, "Stopping EmailWorker");

            Notifyd.Core.Logs.Logger.LogInfo("Stopping EmailWorker");
            // Close the connection to Service Bus Queue
            Client.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}
