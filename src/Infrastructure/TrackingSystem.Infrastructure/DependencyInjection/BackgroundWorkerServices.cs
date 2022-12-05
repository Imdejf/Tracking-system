using Microsoft.Extensions.DependencyInjection;

namespace TrackingSystem.Infrastructure.DependencyInjection
{
    public static partial class IServiceCollectionExtension
    {
        public static IServiceCollection AddBackgroundWorker(this IServiceCollection services)
        {
            services
                .AddHostedService<Infrastructure.BackgroundWorker.BackgroundWorker>();

            return services;
        }
    }
}
