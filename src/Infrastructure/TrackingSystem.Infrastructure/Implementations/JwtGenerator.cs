using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Common.Factories.ApplicationModelsFactories;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Models;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Shared.Configurations;
using TrackingSystem.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;

namespace TrackingSystem.Infrastructure.Implementations
{
    internal sealed class JwtGenerator : IJwtGenerator
    {
        private readonly JwtServiceConfig _Config;
        public JwtGenerator(IOptions<JwtServiceConfig> options)
        {
            _Config = options.Value;
        }

        public JwtGenerationResult Generate(UserDTO dto)
        {
            var newClaims = JwtClaimsFactory.CreateFromIntranetUserDTO(dto);
            return internalCreate(newClaims);
        }

        private JwtGenerationResult internalCreate(JustCommerceJwtClaims claims)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_Config.TokenLifetimeInMinutes);
            var signingCredentials = createSigningCredentials(_Config.GetRsaPrivateKey());
            var jwt = createJwtSecurityToken(claims, signingCredentials, now, expires);
            return new JwtGenerationResult
            {
                ExpireTimeInMs = (expires - now).TotalMilliseconds,
                Jwt = new JwtSecurityTokenHandler().WriteToken(jwt)
            };
        }

        private SigningCredentials createSigningCredentials(RSA rsa)
        {
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };
            return signingCredentials;
        }

        private JwtSecurityToken createJwtSecurityToken(JustCommerceJwtClaims jwtClaims, SigningCredentials signingCredentials, DateTime from, DateTime to)
        {
            var jwt = new JwtSecurityToken(
                audience: _Config.Audience,
                issuer: _Config.Issuer,
                claims: generateClaimsArray(jwtClaims, from),
                notBefore: from,
                expires: to,
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private Claim[] generateClaimsArray(JustCommerceJwtClaims jwtClaims, DateTime from)
        {
            var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(from).ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(
                jwtClaims.Permissions
                .Select(c => new Claim(c.Key, c.Value.ToString(), ClaimValueTypes.UInteger64))
                .ToList());

            claims.AddRange(
                jwtClaims.PermissionsList
               .SelectMany(c => c.Value.Select(a => new Claim(c.Key + "LIST**", a.ToString(), ClaimValueTypes.UInteger64)))
               .ToList());

            foreach (PropertyInfo prop in jwtClaims.GetType().GetProperties())
            {
                if (!claims.Any(c => c.Type == prop.Name) && !prop.PropertyType.IsGenericType)
                {
                    claims.Add(new Claim(prop.Name, prop.GetValue(jwtClaims).ToString(), prop.PropertyType.ToString()));
                }
            }

            return claims.ToArray();
        }
    }
}
