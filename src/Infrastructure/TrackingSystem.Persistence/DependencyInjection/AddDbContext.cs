using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Persistence.DataAccess;
using TrackingSystem.Shared.DependencyInjection;

namespace TrackingSystem.Persistence.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJustCommerceDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TrackingSystemDbContext>(
              options =>
              {
                  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                  options.LogTo(System.Console.WriteLine);
                  options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
              }
          );
            services.AddTrackingDisposeBehaviour<TrackingSystemDbContext>();
            services.AddScoped<IUnitOfWork, TrackingSystemDbContext>();

            return services;
        }

        public static IServiceCollection AddInMemoryDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TrackingSystemDbContext>(
                options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                    options.LogTo(System.Console.WriteLine);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                }
            );
            services.AddTrackingDisposeBehaviour<TrackingSystemDbContext>();
            services.AddScoped<IUnitOfWork, TrackingSystemDbContext>();


            return services;
        }
    }
}
