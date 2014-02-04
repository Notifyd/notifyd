using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Models 
{
    public class MessageEntity : DataEntity
    {
        public MessageEntity(string orgId, string messageId)
        {
            base.PartitionKey = orgId;
            base.RowKey = messageId;
        }

        public string Body { get; set; }

        public string ToAddress { get; set; }

        public string FromAddress { get; set; }

        public string FromDisplay { get; set; }

        public string Subject { get; set; }

        public string Transport { get; set; }

        public string Status { get; set; }

        public string SubmittedOn { get; set; }

        public string CompletedOn { get; set; }

    }
}
