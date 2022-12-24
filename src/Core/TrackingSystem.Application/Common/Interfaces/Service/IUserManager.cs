using TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Models;
using TrackingSystem.Application.Models;
using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess.Service
{
    public interface IUserManager
    {
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<IdentityActionResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword, CancellationToken cancellationToken);
        Task<IdentityActionResult> LoginAsync(UserEntity user, string password, CancellationToken cancellationToken);
        Task<bool> IsEmailTakenAsync(string email, CancellationToken cancellationToken);
        Task<(UserEntity, IdentityActionResult)> RegisterAsync(UserEntity user, string password);
        Task<IdentityActionResult> ResetPasswordAsync(UserEntity user, string password, string passwordResetToken, CancellationToken cancellationToken);
        Task<IdentityActionResult> ConfirmEmailAsync(UserEntity user, string emailConfirmationToken, CancellationToken cancellationToken);
        Task RemoveAccountAsync(UserEntity user, CancellationToken cancellationToken);
        Task<UserEntity> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<UserEntity>> GetAlUser(Guid userId, CancellationToken cancellationToken);
        Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserEntity?> GetUserByMailOrNameAsync(string mailOrName, CancellationToken cancellationToken);
        Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken = default);
        Task UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<IdentityActionResult> ChangePasswordByAdminAsync(UserEntity user, string password, CancellationToken cancellationToken);

    }
}
