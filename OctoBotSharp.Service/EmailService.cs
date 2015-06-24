using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OctoBotSharp.Service
{
    public class IdentityEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(message.Destination));
            mail.Body = message.Body;
            mail.Subject = message.Subject;
            mail.From = new MailAddress("DoNotReply@OctoBotSharp.com");

            using (var client = new SmtpClient())
            {
                client.Send(mail);
            }

            return Task.FromResult(0);
        }
    }
}
