using TrackingSystem.Domain.Entities.Email;
using TrackingSystem.Domain.Enums;

namespace TrackingSystem.Application.Common.Interfaces
{
    public interface IMailSender
    {
        Task SendPasswordResetEmailAsync(string reciverEmail, string passwordResetToken, Guid userId, CancellationToken cancellationToken);
        Task SendEmailConfirmationEmailAsync(string reciverEmail, string emailConfirmationToken, Guid userId, CancellationToken cancellationToken);
    }
}
