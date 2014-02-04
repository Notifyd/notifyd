using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Models
{
   public  class LogEntry : DataEntity 
    {
       public LogEntry(string orgId, string messageId)
        {
            base.PartitionKey = orgId;
            base.RowKey = messageId;
        }

       public string Message { get; set; }
       public string Level { get; set; }
       public string Source { get; set; }
    }
}
