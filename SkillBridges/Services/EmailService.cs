using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace SkillBridges.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public  void SendEmail(string toEmail,string subject,string body)
        {
            using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl
            };

            var mail = new MailMessage()
            {
                From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);
            client.Send(mail);
        }
    }
}
