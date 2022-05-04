using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dnata.Automation.BDDFramework.Helpers
{
    static class EmailHelper
    {

        public static void SendMailWithAttachment(string filename, string fromEmail,string toEmail, string reportTitle, string emailServer, string reportBody)
        {
                MailAddress ma = new MailAddress(fromEmail);
                MailMessage message = new MailMessage();
                message.From = ma;

                for (int counter = 1; counter <= toEmail.Split(';').Length; counter++)
                    message.To.Add(toEmail.Split(';')[counter - 1]);

                message.Subject = reportTitle;
                message.Body = reportBody?? reportTitle;
                Attachment att = new Attachment(filename);
                message.Attachments.Add(att);
                SmtpClient smtpClient = new SmtpClient(emailServer);
                smtpClient.Send(message);
        }


        public static void SendMail(string fromEmail, string toEmail, string reportTitle, string emailServer, string reportBody)
        {

            MailAddress ma = new MailAddress(fromEmail);
            MailMessage message = new MailMessage();
            message.From = ma;

            for (int counter = 1; counter <= toEmail.Split(';').Length; counter++)
                message.To.Add(toEmail.Split(';')[counter - 1]);

            message.Subject = reportTitle;
            message.Body = reportBody;
            SmtpClient smtpClient = new SmtpClient(emailServer);
            smtpClient.Send(message);
        }
    }
}
