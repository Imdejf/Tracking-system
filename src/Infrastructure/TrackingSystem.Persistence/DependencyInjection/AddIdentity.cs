using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Persistence.Configurations.Identity;
using TrackingSystem.Persistence.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TrackingSystem.Persistence.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {

            services.AddIdentityCore<UserEntity>(options =>
            {
                options.Password = IdentityOptionsConfig.PasswordOptions;
                options.User = IdentityOptionsConfig.UserOptions;
                options.ClaimsIdentity = IdentityOptionsConfig.ClaimsIdentityOptions;
                options.Lockout = IdentityOptionsConfig.LockoutOptions;
                options.Tokens = IdentityOptionsConfig.TokenOptions;
                options.SignIn = IdentityOptionsConfig.SignInOptions;
                options.Stores = IdentityOptionsConfig.StoreOptions;
            })
            .AddEntityFrameworkStores<TrackingSystemDbContext>()
            .AddUserManager<UserManager<UserEntity>>()
            .AddSignInManager<SignInManager<UserEntity>>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
