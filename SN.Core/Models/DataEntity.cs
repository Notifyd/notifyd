using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Models
{
    public class DataEntity : TableEntity, IEntity
    {

        public DataEntity()
        {

        }

        //public DataEntity(string organizationId, string recordId)
        //{
        //    this.PartitionKey = organizationId;
        //    this.RowKey = recordId;
           
        //}

        public string OrganizationId 
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string RecordId
    {
        get
        {
            return this.RowKey;
        }
        set
        {
            this.RowKey = value;
        }
    }

        public DateTime ModifiedOn { get; set; }
    }
}
