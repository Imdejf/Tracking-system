using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Shared.Maps
{
    internal static class ProfilePermissions
    {
        private static IDictionary<Profile, IEnumerable<PermissionObject>> _Permissions = new Dictionary<Profile, IEnumerable<PermissionObject>>()
        {
            {
                Profile.None,
                System.Array.Empty<PermissionObject>()
            },
            {
                Profile.User,
                System.Array.Empty<PermissionObject>()
            },
            {
                Profile.Boss,
                new PermissionObject[]
                {

                }
            },
        };

        public static IEnumerable<PermissionObject> GetProfilePermissions(Profile profile) => _Permissions[profile];
    }
}
