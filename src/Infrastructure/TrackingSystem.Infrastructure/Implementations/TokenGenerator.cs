using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace TrackingSystem.Infrastructure.Implementations
{
    internal sealed class TokenGenerator : ITokenGenerator
    {
        private readonly UserManager<UserEntity> _IdentityUserManager;
        public TokenGenerator(UserManager<UserEntity> identityUserManager)
        {
            _IdentityUserManager = identityUserManager;
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return _IdentityUserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public Task<string> GeneratePasswordResetTokenAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return _IdentityUserManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
