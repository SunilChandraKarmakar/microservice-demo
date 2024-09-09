using Ordering.Applications.Contracts.Infrastructure.EmailServices;

namespace Ordering.Infrastructure.EmailService
{
    public class SendEmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(Applications.Models.Email email)
        {
            var sendEmail = new QuickMailer.Email();

            // Check email is valid or not
            if(sendEmail.IsValidEmail(email.To))
                return sendEmail.SendEmail(email.To, "", "", email.Subject, email.Body);

            return false;
        }
    }
}