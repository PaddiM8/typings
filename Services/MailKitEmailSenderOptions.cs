using MailKit.Security;

#nullable disable
namespace Typings.Services
{
    public class MailKitEmailSenderOptions
    {
        public string HostAddress { get; set; }
     
        public int HostPort { get; set; }
     
        public string HostUsername { get; set; }
     
        public string HostPassword { get; set; }
     
        public SecureSocketOptions HostSecureSocketOptions { get; set; }
     
        public string SenderEmail { get; set; }
     
        public string SenderName { get; set; }
        
        public MailKitEmailSenderOptions()
        {
            HostSecureSocketOptions = SecureSocketOptions.Auto;
        }
    }
}
