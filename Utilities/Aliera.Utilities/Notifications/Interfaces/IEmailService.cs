using System.Threading.Tasks;

namespace Aliera.Utilities.Notifications
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
        bool SendEmail(Email email);

    }
}
