using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace Notifyd.Core.Operations
{
    public class MailManager
    {

        public void Send(Models.MessageEntity entity)
        {
            try
            {
                if (UseSendGrid(entity.Transport))
                {
                    // Send via SendGrid
                    Console.WriteLine("---TRACE--- Sending via SendGrid");
                    SendGridSend(entity);
                    
                }else
                {
                    // Send via System.Net.Mail
                    Console.WriteLine("---TRACE--- Sending via System.Net");
                    SystemSend(entity);
                }
            }
            catch
            {

            }
         }

        private Boolean UseSendGrid(string transport)
        {
            transport = transport.ToLower();
            if (transport.Contains("sendgrid"))
            {
                return true;
            }
            else { return false; }
        }

        private void SendGridSend(Models.MessageEntity msg)
        {
            // Create the email object first, then add the properties.
            SendGrid envelope = SendGrid.GetInstance();

            envelope.AddTo(msg.ToAddress);
            envelope.From = new MailAddress(msg.FromAddress, msg.FromDisplay);
            envelope.Subject = msg.Subject;
            envelope.Text = msg.Body;

            //create an instance of the Web transport mechanism
            var transportInstance = Web.GetInstance(new NetworkCredential(Configuration.MasterConfiguration.OperationsSendGridUserName, Configuration.MasterConfiguration.OperationsSendGridPassword));

            //send the mail
            transportInstance.Deliver(envelope);
            Console.WriteLine("---TRACE--- Sent to " + msg.ToAddress);
        }

        private void SystemSend(Models.MessageEntity msg)
        {
            MailMessage mail = new MailMessage();
            mail.Body = msg.Body; 
            mail.IsBodyHtml = false;

            mail.To.Add(new MailAddress(msg.ToAddress));
            mail.From = new MailAddress(msg.FromAddress, msg.FromDisplay, Encoding.UTF8);
            mail.Subject = msg.Subject;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.Normal;
    
            SmtpClient smtp = new SmtpClient();
            //smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
            smtp.Host = Configuration.MasterConfiguration.OperationsSystemMailHost;
            smtp.Send(mail);
        }
        }
    }

