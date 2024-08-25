using Ordering.Applications.Models;

namespace Ordering.Applications.Contracts.Infrastructure.EmailServices
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}