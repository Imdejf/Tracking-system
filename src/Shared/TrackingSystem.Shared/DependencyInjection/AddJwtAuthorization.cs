using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TrackingSystem.Shared.Configurations;
using TrackingSystem.Shared.Services.Implementations.JwtService;
using TrackingSystem.Shared.Services.Interfaces.JwtService;

namespace TrackingSystem.Shared.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, JwtServiceConfig jwtServiceConfig)
        {
            services.AddTransient<IJwtDecoder, JwtDecoder>();
            services.AddTransient<IJwtValidator, JwtValidator>();
            services.Configure<JwtServiceConfig>(c =>
            {
                c.Issuer = jwtServiceConfig.Issuer;
                c.ReferralId = jwtServiceConfig.ReferralId;
                c.ReferralUrl = jwtServiceConfig.ReferralUrl;
                c.RsaPrivateKey = jwtServiceConfig.RsaPrivateKey;
                c.RsaPublicKey = jwtServiceConfig.RsaPublicKey;
                c.TokenLifetimeInMinutes = jwtServiceConfig.TokenLifetimeInMinutes;
                c.Audience = jwtServiceConfig.Audience;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = jwtServiceConfig.AsTokenValidationParameters();
            });

            services.AddAuthorization(options =>
            {
                options.InvokeHandlersAfterFailure = true;
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });
            return services;
        }
    }

    public static partial class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseJwtAuthorization(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
