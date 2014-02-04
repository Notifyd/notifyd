using Notifyd.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.Core.Managers
{
    public class LogManager
    {
        private const string _TableName = "Logs";

        public static void LogInfo(string msg)
        {
          
            TableClient client = new TableClient(_TableName);

            Models.LogEntry l = new Models.LogEntry("0", Convert.ToString(Guid.NewGuid()));

            l.Level = "Info";
            l.Message = msg;
            l.Source = "System";
            client.InsertRow(l);

        }
    }
}
