using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel;

namespace Notifyd.Portal.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [DisplayName("To Address")]
        [DataType(DataType.EmailAddress)]
        public string ToAddress { get; set; }

        [DisplayName("From Address")]
        [DataType(DataType.EmailAddress)]
        public string FromAddress { get; set; }


        [DisplayName("From")]
        public string FromDisplay { get; set; }

        [DisplayName("Last Updated")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; }

        [DisplayName("Updated By")]
        public string UpdatedBy { get; set; }

    }
}