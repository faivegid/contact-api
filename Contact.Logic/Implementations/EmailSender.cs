using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Logic.Implementations
{
    public class EmailSender : IMailSender
    {
        public async Task<Response> SendMailAsync(SendGridMessage msg)
        {
            var sendgridKey = "";
            var client = new SendGridClient(sendgridKey);
            return await client.SendEmailAsync(msg);
        }

        public SendGridMessage CreateMessage(string subject, string message, string email)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@adana.ng", "Fresh fruits available"),
                Subject = subject,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(true, true);
            return msg;
        }

        public string RetrieveMessage(string templateName, params string[] parameters)
        {
            return "";
        }
    }
}
