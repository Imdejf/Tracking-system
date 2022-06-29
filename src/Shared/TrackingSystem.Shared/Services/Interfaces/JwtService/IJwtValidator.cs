namespace TrackingSystem.Shared.Services.Interfaces.JwtService
{
    public interface IJwtValidator
    {
        bool IsValid(string jwt);
    }
}
