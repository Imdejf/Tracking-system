using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Models;
using TrackingSystem.Application.Models;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Persistence.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TrackingSystem.Persistence.Implementations.CommonRepositories
{
    internal sealed class UserManager : IUserManager
    {
        private readonly TrackingSystemDbContext _trackingSystemDbContext;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserManager(TrackingSystemDbContext trackingSystemDbContext, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _trackingSystemDbContext = trackingSystemDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityActionResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return mapIdentityResult(result);
        }

        public async Task<IdentityActionResult> ConfirmEmailAsync(UserEntity user, string emailConfirmationToken, CancellationToken cancellationToken)
        {
            var result = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
            return mapIdentityResult(result);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _trackingSystemDbContext.Users.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _trackingSystemDbContext.Users.Where(c => EF.Functions.Like(c.Email, $"%{email}%")).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<UserEntity?> GetUserByMailOrNameAsync(string mailOrName, CancellationToken cancellationToken)
        {
            return _trackingSystemDbContext.Users
                .AsNoTracking()
                .Where(c => c.UserName == mailOrName || c.Email == mailOrName)
                .Include(c => c.UserPermissions)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<UserEntity?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _trackingSystemDbContext.Users.Where(c => c.Id == userId).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<bool> IsEmailTakenAsync(string email, CancellationToken cancellationToken)
        {
            return _trackingSystemDbContext.Users.AnyAsync(c => EF.Functions.Like(c.Email, $"%{email}%"), cancellationToken);
        }

        public async Task<IdentityActionResult> LoginAsync(UserEntity user, string password, CancellationToken cancellationToken)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return mapIdentityResult(result);
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }


        public async Task<(UserEntity, IdentityActionResult)> RegisterAsync(UserEntity user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return (user, mapIdentityResult(result));
        }


        public async Task RemoveAccountAsync(UserEntity user, CancellationToken cancellationToken)
        {
            _trackingSystemDbContext.Attach(user).State = EntityState.Deleted;
            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IdentityActionResult> ResetPasswordAsync(UserEntity user, string password, string passwordResetToken, CancellationToken cancellationToken)
        {
            var result = await _userManager.ResetPasswordAsync(user, passwordResetToken, password);
            return mapIdentityResult(result);
        }

        private IdentityActionResult mapIdentityResult(IdentityResult identityResult)
        {
            if (identityResult.Succeeded) return IdentityActionResult.Success();
            return IdentityActionResult.Failure(identityResult.Errors.Select(c => $"{c.Code} : {c.Description}").ToArray());
        }
        private IdentityActionResult mapIdentityResult(SignInResult signInResult)
        {
            if (!signInResult.Succeeded)
            {
                List<string> errors = new List<string>();
                if (signInResult.IsNotAllowed)
                {
                    errors.Add("Login is not allowed");
                }
                if (signInResult.IsLockedOut)
                {
                    errors.Add("Account is locked out");
                }
                if (signInResult.RequiresTwoFactor)
                {
                    errors.Add("Account requires to factor");
                }
                return IdentityActionResult.Failure(errors.ToArray());
            }
            return IdentityActionResult.Success();
        }
    }
}
