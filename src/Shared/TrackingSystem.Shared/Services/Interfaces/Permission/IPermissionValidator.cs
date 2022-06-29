using TrackingSystem.Shared.Enums;

namespace TrackingSystem.Shared.Services.Interfaces.Permission
{
    public interface IPermissionValidator
    {
        bool HasPermissions(Type enumType, int requiredPermissions, int ownedPermissions, PermissionValidationMethod method);
        bool HasPermissions(Type enumtype, int requiredPermissions, string jwt, PermissionValidationMethod method);
        bool IsPermissionValid(string domainName, int flagValue);
    }
}
