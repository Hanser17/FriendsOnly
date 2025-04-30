using Application.DTOs.Email;
using Application.Interfaces;
using Domain.Setting;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ShareLayer.Service
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get; }

        // IOptions<MailSettings> injecta la configuracion MailSettings que se definio en appsetting.jason
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
       public async Task SendAsync(EmailRequest emailRequest)
        {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
                email.To.Add(MailboxAddress.Parse(emailRequest.To));
                email.Subject = emailRequest.Subject;
                BodyBuilder bodyBuilder = new();
                bodyBuilder.HtmlBody = emailRequest.Body;
                email.Body = bodyBuilder.ToMessageBody();


               using  SmtpClient smtp = new();
               smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
               smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort,SecureSocketOptions.StartTls );
               smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
               await smtp.SendAsync(email);
               smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
