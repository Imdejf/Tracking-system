using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Services.Interfaces.JwtService;
using System.IdentityModel.Tokens.Jwt;

namespace TrackingSystem.Shared.Services.Implementations.JwtService
{
    internal sealed class JwtDecoder : IJwtDecoder
    {
        public JwtClaims Decode(string jwt)
        {
            if (String.IsNullOrEmpty(jwt))
            {
                throw new ArgumentException("Token cant be NULL or empty string");
            }
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

            return new JwtClaims
            {
                Id = new Guid(decodedToken.Claims.FirstOrDefault(c => c.Type == nameof(JwtClaims.Id)).Value),
                Email = decodedToken.Claims.FirstOrDefault(c => c.Type == nameof(JwtClaims.Email)).Value
            };
        }
    }
}
