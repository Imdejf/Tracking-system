using Microsoft.EntityFrameworkCore;
using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Common.Factories.DtoFactories;
using TrackingSystem.Application.Common.Factories.EntitiesFactories.User;
using TrackingSystem.Application.Common.Interfaces.Service;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Persistence.DataAccess;
using TrackingSystem.Shared.Services.Interfaces.Permission;

namespace TrackingSystem.Persistence.Implementations
{
    internal sealed class UserPermissionManager : IUserPermissionManager
    {
        private readonly TrackingSystemDbContext _trackingSystemDbContext;
        private readonly IPermissionsMapper _permissionsMapper;
        public UserPermissionManager(TrackingSystemDbContext trackingSystemDbContext, IPermissionsMapper permissionsMapper)
        {
            _trackingSystemDbContext = trackingSystemDbContext;
            _permissionsMapper = permissionsMapper;
        }

        public Task<bool> UserHasPermissionAsync(Guid userId, string permissionDomainName, int permissionFlagValue, CancellationToken cancellationToken = default)
        {
            return _trackingSystemDbContext.UserPermission.AnyAsync(c =>
                c.UserId == userId &&
                c.PermissionDomainName == permissionDomainName &&
                c.PermissionFlagValue == permissionFlagValue
                , cancellationToken);
        }

        public async Task RemoveAllUserPermissions(Guid userId, CancellationToken cancellationToken)
        {
            var thisUserAllPermissions = await _trackingSystemDbContext.UserPermission.AsNoTracking().Where(c => c.UserId == userId).ToListAsync(cancellationToken);
            _trackingSystemDbContext.UserPermission.RemoveRange(thisUserAllPermissions);
            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task GrantUserPermissions(IEnumerable<(string, int)> permissions, Guid userId, CancellationToken cancellationToken)
        {
            var permissionsAsEntities = permissions.Select(c => UserPermissionEntityFactory.CreateFromData(c.Item1, c.Item2, userId));
            _trackingSystemDbContext.UserPermission.AddRange(permissionsAsEntities);
            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddPermissionAsync(UserPermissionEntity userPermission, CancellationToken cancellationToken = default)
        {
            await _trackingSystemDbContext.UserPermission.AddAsync(userPermission, cancellationToken);
            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemovePermissionAsync(Guid userId, string permissionDomainName, int permissionFlagValue, CancellationToken cancellationToken)
        {
            var permission = await _trackingSystemDbContext.UserPermission
                .Where(c => c.UserId == userId && c.PermissionDomainName == permissionDomainName && c.PermissionFlagValue == permissionFlagValue)
                .FirstOrDefaultAsync(cancellationToken);

            _trackingSystemDbContext.UserPermission.Remove(permission);

            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemovePermissionRangeAsync(IEnumerable<PermissionDTO> permissions, Guid userId, CancellationToken cancellationToken = default)
        {
            var permissionsToRevokeAsEntities = permissions.Select(c => UserPermissionEntityFactory.CreateFromDtoAndUserId(c, userId));
            _trackingSystemDbContext.UserPermission.RemoveRange(permissionsToRevokeAsEntities);
            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task GrantPermissionRangeAsync(IEnumerable<PermissionDTO> permissions, Guid userId, CancellationToken cancellationToken = default)
        {
            var permissionsToGrantAsEntities = permissions.Select(c => UserPermissionEntityFactory.CreateFromDtoAndUserId(c, userId));
            _trackingSystemDbContext.UserPermission.AddRange(permissionsToGrantAsEntities);
            await _trackingSystemDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<PermissionDTO>> GetOwnedPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var ownedPermissions = await _trackingSystemDbContext.UserPermission
                           .AsNoTracking()
                           .Where(c => c.UserId == userId)
                           .Select(c =>
                               PermissionDtoFactory.CreateFromData(
                                   c.PermissionDomainName,
                                   Enum.GetName(_permissionsMapper.GetPermissionTypeByName(c.PermissionDomainName), c.PermissionFlagValue),
                                   c.PermissionFlagValue)
                            )
                           .ToListAsync(cancellationToken);

            return ownedPermissions;
        }
    }
}
