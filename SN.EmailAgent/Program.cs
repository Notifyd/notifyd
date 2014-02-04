using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notifyd.EmailAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting SN EmailAgent");
            Notifyd.Core.Logs.Logger.LogWarning("Starting up EmailAgent");
           //UnifiedLogging.Client.ULSubmitDataRequest client = new UnifiedLogging.Client.ULSubmitDataRequest("","","","");
         
           // client.SubmitAsync();

            try
            {
                Notifyd.Core.Managers.MessageManager.CreateMessage("bp.com", "Test Client Message", "Hellow World!", "byronpate.com Support", "support@byronpate.com", "byronpate@gmail.com");

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Notifyd.Core.Logs.Logger.LogWarning("Error Creating Message. " + e.ToString(),"EmailAgent");
            }

            Notifyd.Core.Logs.Logger.LogWarning("Stopping EmailAgent", "EmailAgent");
            Console.WriteLine("Ending SN EmailAgent");
            Notifyd.Core.Logs.Logger.LogError("Ending EmailAgent");
            Console.ReadLine();
        }

        static void SendMessage()
        {
            // Create the email object first, then add the properties.
            SendGrid myMessage2 = SendGrid.GetInstance();

            myMessage2.AddTo("byronpate@gmail.com");
            myMessage2.From = new MailAddress("john@example.com", "John Smith");
            myMessage2.Subject = "Testing the SendGrid Library";
            myMessage2.Text = "Hello World!";

            //create an instance of the Web transport mechanism
            var transportInstance = Web.GetInstance(new NetworkCredential("sn_svc", "Dec2011!"));

            //send the mail
            transportInstance.Deliver(myMessage2);
        }

        static Notifyd.Core.Models.MessageEntity CreateEntity()
        {
            Notifyd.Core.Models.MessageEntity msg = new Notifyd.Core.Models.MessageEntity("0",Convert.ToString(Guid.NewGuid()));
            
            msg.Subject = "Agent Test";
            msg.Status = "1";
            msg.Transport = "sendgrid";
            msg.Body = "Test sent @ " + System.DateTime.Now.ToString();
            msg.FromDisplay = "Notifyd";
            msg.FromAddress = "noreply@notifyd.net";
            msg.ToAddress = "bypate@coca-cola.com";
            msg.SubmittedOn = System.DateTime.Now.ToString();
          
            return msg;
        }
    }
}
