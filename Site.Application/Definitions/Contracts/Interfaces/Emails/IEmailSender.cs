using Site.Application.Definitions.Models.Emails;

namespace Site.Application.Definitions.Contracts.Interfaces.Emails
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
