using TrackingSystem.Shared.Models;

namespace TrackingSystem.Shared.Services.Interfaces.JwtService
{
    public interface IJwtDecoder
    {
        JwtClaims Decode(string jwt);
    }
}
