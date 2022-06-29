using TrackingSystem.Domain.Entities.Abstract;

namespace TrackingSystem.Domain.Entities.Email
{
    public sealed class EmailAccountEntity : Entity
    {
        public string Name { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public string Pop3Server { get; set; } = String.Empty;
        public int? Pop3Port { get; set; }
        public string Pop3Login { get; set; } = String.Empty;
        public string Pop3Password { get; set; } = String.Empty;
        public string SmtpServer { get; set; } = String.Empty;
        public int? SmtpPort { get; set; }
        public string SmtpLogin { get; set; } = String.Empty;
        public string SmtpPassword { get; set; } = String.Empty;
        public string ImapServer { get; set; } = String.Empty;
        public int? ImapPort { get; set; }
        public string ImapLogin { get; set; }
        public string ImapPassword { get; set; }

        public EmailAccountEntity() { }

        public EmailAccountEntity(string id,string shopId, string? name, string? emailAdress, string? pop3Server, int? pop3Port, string? pop3Login, string? pop3Password, string? smtpServer, int? smtpPort, string? smtpLogin, string? smtpPassword,
                                  string? imapServer, int? imapPort, string? imapLogin, string? imapPassword)
        {
            Id = Guid.Parse(id);
            Name = name;
            EmailAddress = emailAdress;
            Pop3Server = pop3Server;
            Pop3Port = pop3Port;
            Pop3Password = pop3Password;
            Pop3Login = Pop3Login;
            SmtpServer = smtpServer;
            SmtpPort = smtpPort;
            SmtpLogin = smtpLogin;
            SmtpPassword = smtpPassword;
            ImapServer = imapServer;
            ImapPort = imapPort;
            ImapLogin = imapLogin;
            ImapPassword = imapPassword;
        }
    }
}
