using DataSharp.EmailSender.DependencyInjection;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.CommonServices;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Notification;
using TrackingSystem.Infrastructure.Configurations;
using TrackingSystem.Infrastructure.Implementations;
using TrackingSystem.Infrastructure.Implementations.Common;
using TrackingSystem.Infrastructure.Implementations.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TrackingSystem.Infrastructure.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSenderConfig = new DataSharp.EmailSender.Models.EmailSenderConfig();
            configuration.GetSection("Smtp").Bind(emailSenderConfig);

            services.AddEmailSender(emailSenderConfig);
            services.AddEmailTemplateProvider(c =>
            {
                c.AddTemplate("EmailConfirmation", a =>
                    a.WithHtmlBodyFromFile("EmailTemplates/EmailConfirmation.html")
                     .AddReplacementKey("[EMAILADDRESS]")
                     .AddReplacementKey("[URL]")
                );
                c.AddTemplate("PasswordReset", a =>
                    a.WithHtmlBodyFromFile("EmailTemplates/PasswordReset.html")
                     .AddReplacementKey("[EMAILADDRESS]")
                     .AddReplacementKey("[URL]")
                );
            });

            services.Configure<MailLinksConfig>(configuration.GetSection("MailLinks"));
            services.AddTransient<IMailSender, MailSender>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<INotificationHubClientWrapper, NotificationHubClientWrapper>();

            services.AddSingleton<IUserIdsManager, UserIdsManager>();
            services.AddSingleton<INotificationHubClient, NotificationHubClient>();

            services.AddEmailTemplateProvider(c => { });

            return services;
        }
    }
}
