using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Enums.Permissions;
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
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.ChangeOwnPassword.ToString(),(int)AuthServicePermissions.ChangeOwnPassword),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.ChangePassword.ToString(),(int)AuthServicePermissions.ChangePassword),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.ChangeRole.ToString(),(int)AuthServicePermissions.ChangeRole),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.CreateUser.ToString(),(int)AuthServicePermissions.CreateUser),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.DeleteUser.ToString(),(int)AuthServicePermissions.DeleteUser),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.EditUser.ToString(),(int)AuthServicePermissions.EditUser),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.ManagePermissions.ToString(),(int)AuthServicePermissions.ManagePermissions),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.SetUserActiveOrDeactive.ToString(),(int)AuthServicePermissions.SetUserActiveOrDeactive),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.ViewContactList.ToString(),(int)AuthServicePermissions.ViewContactList),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.ViewManagementList.ToString(),(int)AuthServicePermissions.ViewManagementList),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.RevokePermission.ToString(),(int)AuthServicePermissions.RevokePermission),
                    new(typeof(AuthServicePermissions).Name,AuthServicePermissions.GrantPermission.ToString(),(int)AuthServicePermissions.GrantPermission),

                }
            },
        };

        public static IEnumerable<PermissionObject> GetProfilePermissions(Profile profile) => _Permissions[profile];
    }
}
