namespace TrackingSystem.Application.Common.Extension
{
    public static class RegexExtension
    {
        public static readonly string PasswordValidationRegex = "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*_+=-?])";
    }
}
