using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLogging.Client;

namespace Notifyd.Core.Logs
{
   public class Logger
    {
       private const string _AccessKey = "EF9E0EE2FF8C4A3E9711";
       private const string _SecretKey = "ZDA0YWUxYjk5N2YwNGQxNmFhNmZmNWVmNDc2NjJkNjY=";
       private const string _URL = "https://data-us.unifiedlogging.com/ulmessages/";

       // initializeData="AccessKey=EF9E0EE2FF8C4A3E9711, SecretKey=ZDA0YWUxYjk5N2YwNGQxNmFhNmZmNWVmNDc2NjJkNjY=, SubmissionUrl=https://data-us.unifiedlogging.com/ulmessages/" >

       private ULSubmitDataRequest ulClient;

       public Logger(string message)
       {
           ulClient = new UnifiedLogging.Client.ULSubmitDataRequest(_AccessKey, _SecretKey, _URL, message);
       }

       public static void LogInfo(string msg, string component = "Notifyd.Core")
       {
           // Logger l = new Logger();
           StringBuilder output = new StringBuilder();

           output.AppendLine("SEVERITY:INFO");
           output.AppendLine("MACHINE:" + System.Environment.MachineName);
           output.AppendLine("COMPONENT:" + component);
           output.AppendLine(msg);

           Logger l = new Logger(output.ToString());
           l.ulClient.SubmitAsync();
       }

       public static void LogError(string msg, string component = "Notifyd.Core")
       {
           // Logger l = new Logger();
           StringBuilder output = new StringBuilder();

           output.AppendLine("SEVERITY:ERROR");
           output.AppendLine("MACHINE:" + System.Environment.MachineName);
           output.AppendLine("COMPONENT:" + component);
           output.AppendLine(msg);

           Logger l = new Logger(output.ToString());
           l.ulClient.SubmitAsync();
        
       }

       public static void LogWarning(string msg, string component = "Notifyd.Core")
       {
           // Logger l = new Logger();
           StringBuilder output = new StringBuilder();

           output.AppendLine("SEVERITY:WARNING");
           output.AppendLine("MACHINE:" + System.Environment.MachineName);
           output.AppendLine("COMPONENT:" + component);
           output.AppendLine(msg);

           Logger l = new Logger(output.ToString());
           l.ulClient.SubmitAsync();

       }
    
    }
}
