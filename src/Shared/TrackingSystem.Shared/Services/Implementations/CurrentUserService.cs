using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Services.Interfaces;
using TrackingSystem.Shared.Services.Interfaces.JwtService;

namespace TrackingSystem.Shared.Services.Implementations
{
    internal sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IJwtValidator _JwtValidator;
        private readonly IJwtDecoder _JwtDecoder;
        private JwtClaims _DefaultClaims => new JwtClaims
        {
            Id = Guid.Empty,
            Email = "anonymous@email.com"
        };

        public CurrentUserService(IJwtValidator jwtValidator, IJwtDecoder jwtDecoder)
        {
            _JwtValidator = jwtValidator;
            _JwtDecoder = jwtDecoder;
            currentUser = _DefaultClaims;
        }


        public JwtClaims CurrentUser => currentUser;
        private JwtClaims currentUser;

        public void SetCurrentUser(JwtClaims claims)
        {
            currentUser = claims;
        }

        public void SetCurrentUser(string jwt)
        {
            if (_JwtValidator.IsValid(jwt))
            {
                currentUser = _JwtDecoder.Decode(jwt);
            }
            else
            {
                currentUser = _DefaultClaims;
            }
        }
    }
}
