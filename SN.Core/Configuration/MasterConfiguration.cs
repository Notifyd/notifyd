using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Configuration
{
    class MasterConfiguration
    {
        static public string OperationsSendGridUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["Operations.SendGrid.UserName"];
            }
        }
        static public string OperationsSendGridPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["Operations.SendGrid.Password"];
            }
        }

        static public string OperationsSystemMailHost
        {
            get
            {
                return ConfigurationManager.AppSettings["Operations.System.MailHost"];
            }
        }
    }
}
