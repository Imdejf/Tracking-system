using Microsoft.AspNetCore.Identity;

namespace TrackingSystem.Persistence.Configurations.Identity
{
    public static class IdentityOptionsConfig
    {
        public static readonly PasswordOptions PasswordOptions = new()
        {
            RequireDigit = true,
            RequiredLength = 8,
            RequiredUniqueChars = 1,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };

        public static readonly UserOptions UserOptions = new()
        {
            RequireUniqueEmail = true
        };

        public static readonly ClaimsIdentityOptions ClaimsIdentityOptions = new()
        {

        };

        public static readonly LockoutOptions LockoutOptions = new()
        {
            DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10),
            AllowedForNewUsers = true,
            MaxFailedAccessAttempts = 7
        };

        public static readonly SignInOptions SignInOptions = new()
        {
            RequireConfirmedAccount = false,
            RequireConfirmedEmail = true,
            RequireConfirmedPhoneNumber = false
        };

        public static readonly TokenOptions TokenOptions = new()
        {

        };

        public static readonly StoreOptions StoreOptions = new()
        {

        };
    }
}
