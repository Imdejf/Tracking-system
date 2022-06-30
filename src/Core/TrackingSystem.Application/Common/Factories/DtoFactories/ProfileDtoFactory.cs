using TrackingSystem.Application.Common.DTOs;

namespace TrackingSystem.Application.Common.Factories.DtoFactories
{
    public static class ProfileDtoFactory
    {
        public static ProfileDTO CreateFromData(string flagName, int flagValue)
        {
            return new ProfileDTO
            {
                FlagName = flagName,
                FlagValue = flagValue
            };
        }
    }
}
