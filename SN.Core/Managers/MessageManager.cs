using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifyd.Core.Models;
using Notifyd.Core.IO;
using Microsoft.WindowsAzure.Storage.Table;

namespace Notifyd.Core.Managers
{
    public class MessageManager
    {
        private const string _TableName = "Messages";

        public static void CreateMessage(Models.MessageEntity entity)
        {
            Contracts.MessageContract contract = new Contracts.MessageContract(entity.RowKey, entity.PartitionKey);

            TableClient client = new TableClient(_TableName);
            QueueClient qclient = new QueueClient();

            Console.WriteLine("Inserting Entity");
            entity.ModifiedOn = System.DateTime.Now.ToString();
            client.InsertRow(entity);

            Console.WriteLine("Inserting Queue Msg");
            qclient.Send(contract);
        }

        public static void UpdateMessage(Models.MessageEntity entity)
        {
            TableClient client = new TableClient(_TableName);
            entity.ModifiedOn = System.DateTime.Now.ToString();
            client.UpdateRow(entity);
        }

        public static void CreateMessage(string org, string subject, string body, string fromDisplay, string fromAddress, string toAddress)
        {
        
           MessageEntity msg = new MessageEntity(org, Convert.ToString(Guid.NewGuid()));

         
           msg.Subject = subject;
            msg.Status = "1";
            msg.Transport = "sendgrid";
            msg.Body = body;
            msg.FromDisplay =  fromDisplay;
            msg.FromAddress = fromAddress;
            msg.ToAddress =  toAddress;
            msg.SubmittedOn = System.DateTime.Now.ToString();

            Console.WriteLine("Calling Message Manager");
            CreateMessage(msg);
                 
        }
  
        public static MessageEntity GetMessage(string orgId, string messageId)
        {
            TableClient client = new TableClient(_TableName);

            TableOperation operation = TableOperation.Retrieve<MessageEntity>(orgId, messageId);

            // Execute the retrieve operation.
            TableResult tresult = client.Table.Execute(operation);

            Models.MessageEntity entity = (MessageEntity)tresult.Result;
        
            return entity;    
        }
    }
    
}
