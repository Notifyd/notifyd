using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Notifyd.Portal.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}