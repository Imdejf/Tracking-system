using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Services.Interfaces.Permission;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Shared.Services.Implementations.PermissionMapper
{
    internal sealed class PermissionValidator : IPermissionValidator
    {
        public bool HasPermissions(Type enumType, int requiredPermissions, int ownedPermissions, PermissionValidationMethod method)
        {
            return internalHasPermissions(enumType, requiredPermissions, ownedPermissions, method);
        }
        public bool HasPermissions(Type enumType, int requiredPermissions, string jwt, PermissionValidationMethod method)
        {
            var decodedToken = new JwtSecurityTokenHandler().ReadToken(jwt) as JwtSecurityToken;

            int ownedPermissions = decodedToken.Claims
                .Where(c => c.Type.StartsWith(enumType.Name))
                .Select(c => int.Parse(c.Value))
                .First();

            return internalHasPermissions(enumType, requiredPermissions, ownedPermissions, method);
        }
        public bool IsPermissionValid(string domainName, int flagValue)
        {
            var passedType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.Name == domainName && c.IsEnum && c.Namespace.EndsWith(".Permissions"))
                .FirstOrDefault();
            if (passedType is null) return false;
            var passedTypeElements = Enum.GetValues(passedType);
            foreach (var elem in passedTypeElements)
            {
                if ((int)elem == flagValue) return true;
            }
            return false;
        }

        private bool internalHasPermissions(Type enumType, int requiredPermissions, int ownedPermissions, PermissionValidationMethod method)
        {
            var parsedRequiredPermissions = Enum.Parse(enumType, requiredPermissions.ToString());
            var parsedBitSum = parsedRequiredPermissions
                .ToString()
                .Split(',')
                .Select(flag => Enum.Parse(enumType, flag))
                .ToArray();

            bool hasPermissions = false;
            if (method == PermissionValidationMethod.HasAll)
            {
                hasPermissions = parsedBitSum.All(c => isPermissionPresent(ownedPermissions, c));
            }
            if (method == PermissionValidationMethod.HasAny)
            {
                hasPermissions = parsedBitSum.Any(c => isPermissionPresent(ownedPermissions, c));
            }
            return hasPermissions;
        }
        private bool isPermissionPresent<T>(T value, T lookingForFlag)
        {
            int intValue = (int)(object)value;
            int intLookingForFlag = (int)(object)lookingForFlag;
            return ((intValue & intLookingForFlag) == intLookingForFlag);
        }
    }
}
