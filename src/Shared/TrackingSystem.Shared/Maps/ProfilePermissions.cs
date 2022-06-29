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
                Profile.Accountant,
                new PermissionObject[]
                {
                 
                }
            },
            {
                Profile.Boss,
                new PermissionObject[]
                {

                }
            },
            {
                Profile.DeliveryManager,
                new PermissionObject[]
                {

                }
            },
            {
                Profile.DigitalManager,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.PhysicalManager,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.PackageManager,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.SettlementManager,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.MerchManager,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.Marketing,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.LicensorManager,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.NewUser,
                Array.Empty<PermissionObject>()
            },
            {
                Profile.FullUser,
                Array.Empty<PermissionObject>()
            }
        };

        public static IEnumerable<PermissionObject> GetProfilePermissions(Profile profile) => _Permissions[profile];
    }
}
