namespace TrackingSystem.Application.Models
{
    public sealed class JwtGenerationResult
    {
        public string Jwt { get; set; }
        public double ExpireTimeInMs { get; set; }
    }
}
