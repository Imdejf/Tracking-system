using TrackingSystem.Application;
using TrackingSystem.Infrastructure.DependencyInjection;
using TrackingSystem.Persistence.DependencyInjection;
using TrackingSystem.Shared.Configurations;
using TrackingSystem.Shared.DependencyInjection;

namespace TrackingSystem.API
{
    public class Startup
    {
        private IConfiguration _Configuration { get; }
        private bool _IsForTests { get; init; }
        private JwtServiceConfig _JwtServiceConfig { get; init; }

        public Startup(IConfiguration configuration, bool isForTests = false)
        {
            _Configuration = configuration;
            _IsForTests = isForTests;
            _JwtServiceConfig = new JwtServiceConfig();
            _Configuration.GetSection("JWT").Bind(_JwtServiceConfig);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();

            services.AddSharedServices(_Configuration);
            services.AddAppServices(_Configuration);
            services.AddPersistenceService(_Configuration);
            services.AddPermissionsStorage();
            //services.AddHangfire(_Configuration.GetConnectionString("DefaultConnection"));

            services.AddSwaggerDocumentation();
            services.AddFluentValidators(typeof(ApplicationAssemblyEntryPoint).Assembly);
            services.AddMediator(typeof(ApplicationAssemblyEntryPoint).Assembly);

            if (_IsForTests)
            {
                services.AddInMemoryAuthDbContext(System.Guid.NewGuid().ToString());
            }
            else
            {
                services.AddJustCommerceDbContext(_Configuration.GetConnectionString("DefaultConnection"));
            }

            services.AddControllers();
            services.AddJwtAuthorization(_JwtServiceConfig);
            services.AddIdentity();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerOpenAPI(env);

            app.UseJwtAuthorization();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSharedMiddlewares();

            app.UseJwtAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
