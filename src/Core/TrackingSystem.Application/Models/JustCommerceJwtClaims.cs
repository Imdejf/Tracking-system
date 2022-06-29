using System.Security.Claims;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Application.Models
{
    public class JustCommerceJwtClaims : JwtClaims
    {
        public static new JustCommerceJwtClaims CreateFromJwtClaimsCollection(IEnumerable<Claim> claims)
        {
            var mockedJwtClaims = new JustCommerceJwtClaims();
            return new JustCommerceJwtClaims
            {
                Email = claims.FirstOrDefault(c => c.Type == nameof(mockedJwtClaims.Email)).Value,
                FirstName = claims.FirstOrDefault(c => c.Type == nameof(mockedJwtClaims.FirstName)).Value,
                LastName = claims.FirstOrDefault(c => c.Type == nameof(mockedJwtClaims.LastName)).Value,
                UserName = claims.FirstOrDefault(c => c.Type == nameof(mockedJwtClaims.UserName)).Value,
                Id = new Guid(claims.FirstOrDefault(c => c.Type == nameof(mockedJwtClaims.Id)).Value),
                PermissionsList = claims
                    .Where(c => c.Type.EndsWith("LIST**"))
                    .GroupBy(c => c.Type)
                    .ToDictionary(c => c.Key, c => c.Select(c => int.Parse(c.Value)))
            };
        }
    }
}
