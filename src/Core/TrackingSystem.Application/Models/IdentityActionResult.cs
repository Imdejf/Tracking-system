namespace TrackingSystem.Application.Models
{
    public sealed class IdentityActionResult
    {
        public bool IsSuccessful { get; set; }
        public string[] Errors { get; set; }

        public static IdentityActionResult Success()
        {
            return new IdentityActionResult
            {
                IsSuccessful = true,
                Errors = Array.Empty<string>()
            };
        }
        public static IdentityActionResult Failure(string[] errors)
        {
            return new IdentityActionResult
            {
                IsSuccessful = false,
                Errors = errors
            };
        }
    }
}
