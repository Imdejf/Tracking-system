using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Common.Factories.EntitiesFactories;
using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Application.Common.Interfaces.Service
{
    public interface IUserPermissionManager
    {
        Task AddPermissionAsync(UserPermissionEntity userPermission, CancellationToken cancellationToken = default);
        Task RemovePermissionAsync(Guid userId, string permissionDomainName, int permissionFlagValue, CancellationToken cancellationToken = default);
        Task<List<PermissionDTO>> GetOwnedPermissionsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task RemovePermissionRangeAsync(IEnumerable<PermissionDTO> permissions, Guid userId, CancellationToken cancellationToken = default);
        Task GrantPermissionRangeAsync(IEnumerable<PermissionDTO> permissions, Guid userId, CancellationToken cancellationToken = default);
        Task<bool> UserHasPermissionAsync(Guid userId, string permissionDomainName, int permissionFlagValue, CancellationToken cancellationToken = default);
        Task RemoveAllUserPermissions(Guid userId, CancellationToken cancellationToken);
        Task GrantUserPermissions(IEnumerable<(string, int)> permissions, Guid userId, CancellationToken cancellationToken);
    }
}
