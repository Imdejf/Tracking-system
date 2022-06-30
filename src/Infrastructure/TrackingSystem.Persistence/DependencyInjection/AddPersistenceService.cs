using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Service;
using TrackingSystem.Persistence.Implementations;
using TrackingSystem.Persistence.Implementations.CommonRepositories;

namespace TrackingSystem.Persistence.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            //Transient
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserPermissionManager, UserPermissionManager>();

            return services;
        }
    }
}
