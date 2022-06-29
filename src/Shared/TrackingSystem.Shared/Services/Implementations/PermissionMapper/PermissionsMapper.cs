using TrackingSystem.Shared.Maps;
using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Services.Interfaces.Permission;
using System.Reflection;
namespace TrackingSystem.Shared.Services.Implementations.PermissionMapper
{
    internal sealed class PermissionsMapper : IPermissionsMapper
    {
        public IEnumerable<PermissionObject> GetPermissionsAsObjects()
        {
            var allEnumsInPermissionsFolder = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.IsEnum && c.Namespace.EndsWith("Enums.Permissions"));
            
            foreach (var type in allEnumsInPermissionsFolder)
            {
                var enumOfTypeName = type.Name;
                var enumOfTypeElements = Enum.GetValues(type);
                foreach (var elem in enumOfTypeElements)
                {
                    yield return new PermissionObject(enumOfTypeName, Enum.GetName(type, elem), (int)elem);

                }
            }
        }
        public IEnumerable<ProfileObject> GetProfilesAsObjects()
        {
            return Enum.GetValues<Profile>().Select(c => new ProfileObject
            {
                FlagName = c.ToString(),
                FlagValue = (int)c
            });
        }

        public Type GetPermissionTypeByName(string name)
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(c => c.Name == name).FirstOrDefault();
        }

        public IEnumerable<PermissionObject> GetPermissionsByProfile(Profile profile)
        {
            return ProfilePermissions.GetProfilePermissions(profile);
        }
    }
}