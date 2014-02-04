using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace Notifyd.Core.IO
{
    class QueueClient
    {
        Microsoft.ServiceBus.Messaging.QueueClient _Client;
        string _ConnectionString;
        
        //Endpoint=sb://notifyd.servicebus.windows.net/;SharedAccessKeyName=busaccess;SharedAccessKey=PrHBQkNRvUchFLKmFPSBNUgKoIaDDiQQ0PuoyZIZBB0=

        public QueueClient()
        {
            _ConnectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

           _Client = Microsoft.ServiceBus.Messaging.QueueClient.CreateFromConnectionString(_ConnectionString, "messages");
        }

        public void Send(Contracts.MessageContract contract)
        {
            
            BrokeredMessage msg = new BrokeredMessage(contract);

            //ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http;
           
            System.Net.WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            _Client.Send(msg);
             
        }
    }
}
