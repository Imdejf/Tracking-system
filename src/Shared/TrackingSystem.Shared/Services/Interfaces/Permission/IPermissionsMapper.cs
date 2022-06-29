using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Enums;

namespace TrackingSystem.Shared.Services.Interfaces.Permission
{
    public interface IPermissionsMapper
    {
        IEnumerable<PermissionObject> GetPermissionsAsObjects();
        IEnumerable<ProfileObject> GetProfilesAsObjects();
        IEnumerable<PermissionObject> GetPermissionsByProfile(Profile profile);
        Type GetPermissionTypeByName(string name);
    }
}
