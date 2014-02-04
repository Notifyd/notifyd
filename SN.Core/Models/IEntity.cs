using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Models
{
    public interface IEntity : ITableEntity
    {

        string OrganizationId { get; set; }
        string RecordId { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
