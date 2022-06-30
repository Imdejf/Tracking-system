using DataSharp.EmailSender.Interfaces;
using TrackingSystem.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using TrackingSystem.Infrastructure.Configurations;

namespace TrackingSystem.Infrastructure.Implementations
{
    internal sealed class MailSender : IMailSender
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly MailLinksConfig _linksConfig;
        public MailSender(IEmailSender emailSender, IEmailTemplateProvider emailTemplateProvider, IOptions<MailLinksConfig> linksConfigOptions)
        {
            _emailSender = emailSender;
            _emailTemplateProvider = emailTemplateProvider;
            _linksConfig ??= linksConfigOptions?.Value;
        }

        public Task SendEmailConfirmationEmailAsync(string reciverEmail, string emailConfirmationToken, Guid userId, CancellationToken cancellationToken)
        {
            var callbackUrl = new Uri($"{_linksConfig.EmailConfirmationLink}?token={emailConfirmationToken}&UserId={userId}");

            var htmlToSend = _emailTemplateProvider.BuildTemplate("EmailConfirmation", reciverEmail, callbackUrl.ToString());

            return _emailSender.SendAsync(c =>
                c.From(_emailSender.DefaultSenderAddress, _emailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(htmlToSend)
                 .IsBodyHtml(true)
                 .WithSubject("JustWin : Potwierdzenie adresu email")
            );
        }

        public Task SendPasswordResetEmailAsync(string reciverEmail, string passwordResetToken, Guid userId, CancellationToken cancellationToken)
        {
            var callbackUrl = new Uri($"{_linksConfig.PasswordResetLink}?token={passwordResetToken}&UserId={userId}");

            var htmlToSend = _emailTemplateProvider.BuildTemplate("PasswordReset", reciverEmail, callbackUrl.ToString());

            return _emailSender.SendAsync(c =>
                c.From(_emailSender.DefaultSenderAddress, _emailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(htmlToSend)
                 .IsBodyHtml(true)
                 .WithSubject("JustWin : Reset hasła")
            );
        }
    }
}
