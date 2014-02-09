using Notifyd.Core.Managers;
using Notifyd.Core.Models;
using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TropoCSharp.Structs;
using TropoCSharp.Tropo;
using System.Web;
using System.Xml;
using HastyAPI.Nexmo;
using Twilio;
using Notifyd.Core.Configuration;

namespace Notifyd.EmailAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting SN EmailAgent");
            Notifyd.Core.Logs.Logger.LogWarning("Starting up EmailAgent");

            List<string> arguments = args.ToList<string>();

            if (arguments.Count > 0)
            {
                switch (arguments[0].ToUpper())
                {
                    case "CREATE":
                        Create();
                        break;
                    case "UPDATE":
                        Update();
                        break;
                    case "SMS":
                        SmsTest2();
                        break;
                    case "PUSH":
                        PushTest();
                        break;
                    default:
                        Console.WriteLine("Unknown command: " + arguments[0]);
                        break;
                }
                    

            }
            else
            {
                Console.WriteLine("Available Commands:");
                Console.WriteLine("   CREATE");
                Console.WriteLine("   UPDATE");

            }
            Notifyd.Core.Logs.Logger.LogWarning("Stopping EmailAgent", "EmailAgent");
            Console.WriteLine("Ending SN EmailAgent");
            System.Threading.Thread.Sleep(5000);
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

        static void Create()
        {
            MessageManager.CreateMessage("bp.com", "Test Client Message", "Hellow World! @ " + DateTime.Now.ToString(), "byronpate.com Support", "support@byronpate.com", "byronpate@gmail.com");
        }

        static void Update()
        {
            TestMessageManager();
        }
        static void TestMessageManager()
        {
            MessageEntity msg;
            string orgId = "bp.com";
            string msgId = "a67794a4-9103-4783-974c-49c9f0c3ca91";

            msg = MessageManager.GetMessage(orgId, msgId);

            Console.WriteLine("Loaded: " + msg.Subject);
            msg.Status = "UPDATED";

            MessageManager.UpdateMessage(msg);
          
        }

        static void SMSTest()
        {

            // The voice and messaging tokens provisioned when your Tropo application is set up.
            string voiceToken = "24cfa15afd01ab4489c7b18a0480219c850af8c4d484deb1978682945803be78c8238c963a7541d19c6745fe";
            string messagingToken = "24cfab7873517444b6458ab707aed2a778989481fc39c5100008b7b3a193b0aa05d0ce9e8aa3422862ced55c";

            // A collection to hold the parameters we want to send to the Tropo Session API.
            IDictionary<string, string> parameters = new Dictionary<String, String>();

            // Enter a phone number to send a call or SMS message to here.
            parameters.Add("sendToNumber", "14046416658");

            // Enter a phone number to use as the caller ID.
            parameters.Add("sendFromNumber", "17708290383");

            // Select the channel you want to use via the Channel struct.
            string channel = Channel.Text;
            parameters.Add("channel", channel);

            string network = Network.SMS;
            parameters.Add("network", network);

            // Message is sent as a query string parameter, make sure it is properly encoded.
            parameters.Add("msg", System.Web.HttpUtility.UrlEncode("This is a test message from C#."));

            // Instantiate a new instance of the Tropo object.
            Tropo tropo = new Tropo();

            // Create an XML doc to hold the response from the Tropo Session API.
            XmlDocument doc = new XmlDocument();

            // Set the token to use.
            string token = channel == Channel.Text ? messagingToken : voiceToken;

            // Load the XML document with the return value of the CreateSession() method call.
            doc.Load(tropo.CreateSession(token, parameters));

            // Display the results in the console.
            Console.WriteLine("Result: " + doc.SelectSingleNode("session/success").InnerText.ToUpper());
            Console.WriteLine("Token: " + doc.SelectSingleNode("session/token").InnerText);
            Console.Read();

        }

        static void SmsTest2()
        {
           // var response = new Nexmo("31aa2991", "22484278").Send("19282389105", "14046416658", "Notifyd Test");
           // Console.WriteLine("Response: " + response.MessageCount);
            // Find your Account Sid and Auth Token at twilio.com/user/account
            string AccountSid = "ACecfc4d351f02686d31a8b4b6af014a13";
            string AuthToken = "4a656d817d5f3f7a2b978c99d355c251";

            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            var message = twilio.SendSmsMessage("+14048007204", "+14046416658", "Notifyd Test @ " + DateTime.Now.ToString(), "");

            Console.WriteLine(message.Sid);
        }

        static void PushTest()
        {
            PushoverClient.Pushover pclient = new PushoverClient.Pushover(MasterConfiguration.PushoverAppAPIKey);
         
                    PushoverClient.PushResponse response = pclient.Push(
              "Notifyd Notification", 
              "This is the test message.",
              "uiMFPuuVUTQiQu28YRKgi3vxkU5rbS"
          );

        }
    }
}
