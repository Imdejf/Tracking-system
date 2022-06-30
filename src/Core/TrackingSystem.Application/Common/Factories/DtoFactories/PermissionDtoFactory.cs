using TrackingSystem.Application.Common.DTOs;

namespace TrackingSystem.Application.Common.Factories.DtoFactories
{
    public static class PermissionDtoFactory
    {
        public static PermissionDTO CreateFromData(string domainName, string flagName, int flagValue)
        {
            return new PermissionDTO
            {
                PermissionDomainName = domainName,
                PermissionFlagValue = flagValue,
                PermissionFlagName = flagName
            };
        }
    }
}
