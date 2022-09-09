using TrackingSystem.Shared.Enums;

namespace TrackingSystem.Application.Common.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? UserName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? FilePath { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Profile Profile { get; set; }
        public IDictionary<string, IEnumerable<int>>? Permissions { get; set; }
    }
}
