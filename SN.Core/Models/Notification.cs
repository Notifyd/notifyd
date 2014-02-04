using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Notifyd.Core.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ToAddress { get; set; }

        [DataType(DataType.EmailAddress)]
        public string FromAddress { get; set; }

        public string FromDisplay { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
}