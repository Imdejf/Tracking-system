using DataSharp.EmailSender.Implementations;
using DataSharp.EmailSender.Interfaces;
using DataSharp.EmailSender.Models;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Infrastructure.Configurations;
using TrackingSystem.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace JustCommerce.Infrastructure.Implementations
{
    internal sealed class MailSender : IMailSender
    {
        private readonly IEmailTemplateProvider _dataSharpEmailTemplateProvider;
        private readonly MailLinksConfig _linksConfig;
        private readonly JustCommerceDbContext _justCommerceDbContext;

        public MailSender(IEmailTemplateProvider dataSharpEmailTemplateProvider, IOptions<MailLinksConfig> linksConfig, JustCommerceDbContext justCommerceDbContext)
        {
            _dataSharpEmailTemplateProvider = dataSharpEmailTemplateProvider;
            _linksConfig ??= linksConfig?.Value;
            _justCommerceDbContext = justCommerceDbContext;
        }
        public async Task SendEmailConfirmationEmailAsync(string reciverEmail, string emailConfirmationToken, Guid userId, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default)
        {
            var callbackUrl = new Uri($"{_linksConfig.EmailConfirmationLink}?token={emailConfirmationToken}&UserId={userId}");

            var emailTemplate = await _justCommerceDbContext._EmailTemplate.Include(c => c.EmailAccount).Where(c => c.EmailType == emailType && c.ShopId == shopId).FirstAsync();

            EmailTemplate templateToSend = EmailTemplate.New
                .WithHtmlBodyFromFile(emailTemplate.FilePath)
                .AddReplacementKey("[EMAILADDRESS]")
                .AddReplacementKey("[URL]");

            var templateBody = _dataSharpEmailTemplateProvider.BuildTemplate(templateToSend,reciverEmail, callbackUrl.ToString());

            var options = emailSenderConfiguration(emailTemplate.EmailAccount.SmtpServer, emailTemplate.EmailAccount.SmtpPort, emailTemplate.EmailAccount.SmtpLogin,
                                                   emailTemplate.EmailAccount.SmtpPassword, emailTemplate.EmailAccount.EmailAddress, emailTemplate.EmailAccount.Name, false);

            IEmailSender _dataSharpEmailSender = new EmailSender(options);

            await _dataSharpEmailSender.SendAsync(c =>
                c.From(_dataSharpEmailSender.DefaultSenderAddress, _dataSharpEmailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(templateBody)
                 .IsBodyHtml(true)
                 .WithSubject(emailTemplate.Subject)
            );
        }

        public async Task SendEmailOfferAsync(string reciverEmail, Guid shopId, EmailType emailType, string offerNumber,byte[] offerAttachment, CancellationToken cancellationToken = default)
        {
            var emailTemplate = await _justCommerceDbContext._EmailTemplate.Include(c => c.EmailAccount).Where(c => c.EmailType == emailType && c.ShopId == shopId).FirstAsync();

            EmailTemplate templateToSend = EmailTemplate.New
                .WithHtmlBodyFromFile(emailTemplate.FilePath);

            var templateBody = _dataSharpEmailTemplateProvider.BuildTemplate(templateToSend);

            var options = emailSenderConfiguration(emailTemplate.EmailAccount.SmtpServer, emailTemplate.EmailAccount.SmtpPort, emailTemplate.EmailAccount.SmtpLogin,
                                                   emailTemplate.EmailAccount.SmtpPassword, emailTemplate.EmailAccount.EmailAddress, emailTemplate.EmailAccount.Name, true);

            Attachment att = new Attachment(new MemoryStream(offerAttachment), $"{offerNumber}.pdf");

            IEmailSender _dataSharpEmailSender = new EmailSender(options);

            await _dataSharpEmailSender.SendAsync(c =>
                c.From(_dataSharpEmailSender.DefaultSenderAddress, _dataSharpEmailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(templateBody)
                 .IsBodyHtml(true)
                 .WithAttachment(att)
                 .WithSubject("eMagazynowo : Oferta handlowa"));
        }

        public async Task SendEmailOrderConfirm(string reciverEmail, int orderNumber, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default)
        {
            var emailTemplate = await _justCommerceDbContext._EmailTemplate.Include(c => c.EmailAccount).Where(c => c.EmailType == emailType && c.ShopId == shopId).FirstAsync();

            EmailTemplate templateToSend = EmailTemplate.New
                .WithHtmlBodyFromFile(emailTemplate.FilePath);

            var templateBody = _dataSharpEmailTemplateProvider.BuildTemplate(templateToSend);

            var options = emailSenderConfiguration(emailTemplate.EmailAccount.SmtpServer, emailTemplate.EmailAccount.SmtpPort, emailTemplate.EmailAccount.SmtpLogin,
                                       emailTemplate.EmailAccount.SmtpPassword, emailTemplate.EmailAccount.EmailAddress, emailTemplate.EmailAccount.Name, true);

            IEmailSender _dataSharpEmailSender = new EmailSender(options);

            await _dataSharpEmailSender.SendAsync(c =>
                c.From(_dataSharpEmailSender.DefaultSenderAddress, _dataSharpEmailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(templateBody)
                 .IsBodyHtml(true)
                 .WithSubject(emailTemplate.Subject));
        }

        public async Task SendEmailSetOrderStatusAsync(string reciverEmail, int orderNumber, Guid shopId,EmailType emailType, CancellationToken cancellationToken = default)
        {
            var emailTemplate = await _justCommerceDbContext._EmailTemplate.Include(c => c.EmailAccount).Where(c => c.EmailType == emailType && c.ShopId == shopId).FirstAsync();

            EmailTemplate templateToSend = EmailTemplate.New
                .WithHtmlBodyFromFile(emailTemplate.FilePath);

            var templateBody = _dataSharpEmailTemplateProvider.BuildTemplate(templateToSend);

            var options = emailSenderConfiguration(emailTemplate.EmailAccount.SmtpServer, emailTemplate.EmailAccount.SmtpPort, emailTemplate.EmailAccount.SmtpLogin,
                                                   emailTemplate.EmailAccount.SmtpPassword, emailTemplate.EmailAccount.EmailAddress, emailTemplate.EmailAccount.Name, true);

            IEmailSender _dataSharpEmailSender = new EmailSender(options);

            await _dataSharpEmailSender.SendAsync(c =>
                c.From(_dataSharpEmailSender.DefaultSenderAddress, _dataSharpEmailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(templateBody)
                 .IsBodyHtml(true)
                 .WithSubject(emailTemplate.Subject));
        }

        public async Task SendPasswordResetEmailAsync(string reciverEmail, string passwordResetToken, Guid userId, Guid shopId, EmailType emailType, CancellationToken cancellationToken = default)
        {
            var callbackUrl = new Uri($"{_linksConfig.PasswordResetLink}?token={passwordResetToken}&UserId={userId}");


            var emailTemplate = await _justCommerceDbContext._EmailTemplate.Include(c => c.EmailAccount).Where(c => c.EmailType == emailType && c.ShopId == shopId).FirstAsync();

            EmailTemplate templateToSend = EmailTemplate.New
                .WithHtmlBodyFromFile(emailTemplate.FilePath);

            var templateBody = _dataSharpEmailTemplateProvider.BuildTemplate(templateToSend);

            var options = emailSenderConfiguration(emailTemplate.EmailAccount.SmtpServer, emailTemplate.EmailAccount.SmtpPort, emailTemplate.EmailAccount.SmtpLogin,
                                                   emailTemplate.EmailAccount.SmtpPassword, emailTemplate.EmailAccount.EmailAddress, emailTemplate.EmailAccount.Name, true);

            IEmailSender _dataSharpEmailSender = new EmailSender(options);

            await _dataSharpEmailSender.SendAsync(c =>
                c.From(_dataSharpEmailSender.DefaultSenderAddress, _dataSharpEmailSender.DefaultSenderName)
                 .To(reciverEmail)
                 .WithBody(templateBody)
                 .IsBodyHtml(true)
                 .WithSubject("eMagazynowo : Resetowanie hasła")
            );
        }

        private IOptions<EmailSenderConfig> emailSenderConfiguration(string host, int? port, string login, string password, string defualtEmail, string defualtName, bool ssl)
        {
            EmailSenderConfig EmailSenderConfigInstance = EmailSenderConfig.New
                .OnHost(host)
                .OnPort(port.GetValueOrDefault())
                .OnCredentials(login, password)
                .WithDefaultSenderAddress(defualtEmail)
                .WithDefaultSenderDisplayName(defualtName)
                .EnableSSL(ssl);

            IOptions<EmailSenderConfig> options = Options.Create(EmailSenderConfigInstance);

            return options;
        }
    }
}
