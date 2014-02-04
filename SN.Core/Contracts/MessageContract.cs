using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Contracts
{
    [DataContractAttribute]
    public class MessageContract
    {
        public MessageContract(string rowKey, string partKey)
        {
            this.RowKey = rowKey;
            this.PartitionKey = partKey;
        }

        [DataMemberAttribute]
        public string PartitionKey { get; set; }

        [DataMemberAttribute]
        public string RowKey { get; set; }
    }
}
