using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace TrackingSystem.Shared.Configurations
{
    public class JwtServiceConfig
    {
        public string? ReferralUrl { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? ReferralId { get; set; }
        public string? RsaPrivateKey { get; set; }
        public string? RsaPublicKey { get; set; }
        public int TokenLifetimeInMinutes { get; set; }

        public RSA GetRsaPublicKey()
        {
            if (!String.IsNullOrEmpty(RsaPublicKey))
            {
                var rsa = RSA.Create();
                rsa.FromXmlString(RsaPublicKey);
                return rsa;
            }
            return null;
        }
        public RSA GetRsaPrivateKey()
        {
            if (!String.IsNullOrEmpty(RsaPrivateKey))
            {
                var rsa = RSA.Create();
                rsa.FromXmlString(RsaPrivateKey);
                return rsa;
            }
            return null;
        }
        public TokenValidationParameters AsTokenValidationParameters() => new()
        {
            IssuerSigningKey = new RsaSecurityKey(this.GetRsaPublicKey()),
            ValidAudience = this.Audience,
            ValidIssuer = this.Issuer,
            RequireSignedTokens = true,
            RequireExpirationTime = false,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true
        };
    }
}
