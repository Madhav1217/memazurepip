using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Aliera.Utilities.Notifications
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(Email email)
        {
            return await Task.Run(() => SendEmail(email));
        }
        public bool SendEmail(Email email)
        {
            try
            {
                using (var client = new SmtpClient(email.SmtpServer, email.PortNumber))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(email.UserEmail, email.Password);
                    var mailMessage = new MailMessage();
                    var htmlView = AlternateView.CreateAlternateViewFromString(email.Content, null, "text/html");

                    mailMessage.AlternateViews.Add(htmlView);

                    mailMessage.From = new MailAddress(email.FromMailId);
                    mailMessage.To.Add(email.ToMailId);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = email.MailSubject;
                    client.Send(mailMessage);
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
