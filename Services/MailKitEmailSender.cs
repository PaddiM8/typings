using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Typings.Services
{
    public class MailKitEmailSender : IEmailSender
    {
        public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options)
        {
            Options = options.Value;
        }
     
        public MailKitEmailSenderOptions Options { get; set; }
     
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.Sender = MailboxAddress.Parse(Options.SenderEmail);
            if (!string.IsNullOrEmpty(Options.SenderName))
                mimeMessage.Sender.Name = Options.SenderName;
            mimeMessage.From.Add(mimeMessage.Sender);
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(Options.HostAddress, Options.HostPort, Options.HostSecureSocketOptions);
            await smtp.AuthenticateAsync(Options.HostUsername, Options.HostPassword);
            await smtp.SendAsync(mimeMessage);
            await smtp.DisconnectAsync(true);
        }
    }
}
