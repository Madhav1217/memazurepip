
namespace Aliera.BusinessObjects.Admin
{
    public class NotificationDetailsBO
    {
        public string Type { get; set; }
        public string FromMailId { get; set; }
        public string Content { get; set; }
        public string MailSubject { get; set; }
        public string ToMailId { get; set; }
        public int PortNumber { get; set; }
        public string SmtpServer { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
