using Hangfire;
using Hangfire.MySql;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace TrackingSystem.Shared.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, string connectionString)
        {
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(
                    new MySqlStorage(
                        connectionString,
                        new MySqlStorageOptions
                        {
                            QueuePollInterval = TimeSpan.FromSeconds(10),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = true,
                            DashboardJobListLimit = 25000,
                            TransactionTimeout = TimeSpan.FromMinutes(1),
                            TablesPrefix = "Hangfire",
                        }
                    )
                ));

            // Add the processing server as IHostedService
            services.AddHangfireServer(options => options.WorkerCount = 1);

            return services;
        }
    }
}
