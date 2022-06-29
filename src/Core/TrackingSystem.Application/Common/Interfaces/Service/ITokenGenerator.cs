using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess.Service
{
    public interface ITokenGenerator
    {
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user, CancellationToken cancellationToken);
        Task<string> GeneratePasswordResetTokenAsync(UserEntity user, CancellationToken cancellationToken);
    }
}
