using Microsoft.AspNetCore.Identity;
using TrackingSystem.Domain.Enums;

namespace TrackingSystem.Domain.Entities.Identity
{
    public sealed class UserEntity : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRegisterSource RegisterSource { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<UserPermissionEntity> UserPermissions { get; set; }

    }
}
