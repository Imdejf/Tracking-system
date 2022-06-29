using System.Security.Claims;

namespace TrackingSystem.Shared.Models
{
    public class JwtClaims
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RegisterSource { get; set; }
        public IDictionary<string, IEnumerable<int>> PermissionsList { get; set; }
        public IDictionary<string, int> Permissions => PermissionsList.ToDictionary(c => c.Key.Replace("LIST**", ""), c => c.Value.Sum());

        public static JwtClaims CreateFromJwtClaimsCollection(IEnumerable<Claim> claims)
        {
            var mockedJwtClaims = new JwtClaims();
            return new JwtClaims
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
