using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TrackingSystem.Shared.MediatorPipelineBehaviours;
using TrackingSystem.Shared.Services.Implementations.PermissionMapper;
using TrackingSystem.Shared.Services.Interfaces.Permission;

namespace TrackingSystem.Shared.DependencyInjection
{
    public static class AddPermissionStorage
    {
        public static IServiceCollection AddPermissionsStorage(this IServiceCollection services)
        {
            services.AddTransient<IPermissionValidator, PermissionValidator>();
            services.AddTransient<IPermissionsMapper, PermissionsMapper>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            return services;
        }
    }
}
