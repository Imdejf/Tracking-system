using TrackingSystem.Domain.Entities.Email;
using TrackingSystem.Domain.Enums;

namespace JustCommerce.Application.Common.Interfaces
{
    public interface IMailSender
    {
        Task SendPasswordResetEmailAsync(string reciverEmail, string passwordResetToken, Guid userId, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default);
        Task SendEmailConfirmationEmailAsync(string reciverEmail, string emailConfirmationToken, Guid userId, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default);
        Task SendEmailOfferAsync(string reciverEmail, Guid shopId, EmailType emailType, string offerNumber, byte[] offerAttachment, CancellationToken cancellationToken = default);
        Task SendEmailSetOrderStatusAsync(string reciverEmail, int orderNumber, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default);
        Task SendEmailOrderConfirm(string reciverEmail, int orderNumber, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default);
    }
}
