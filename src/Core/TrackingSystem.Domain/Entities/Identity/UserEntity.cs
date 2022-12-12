using Microsoft.AspNetCore.Identity;
using TrackingSystem.Domain.Entities.Events;
using TrackingSystem.Domain.Entities.Truck;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Enums;

namespace TrackingSystem.Domain.Entities.Identity
{
    public sealed class UserEntity : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string FilePath { get; set; } = String.Empty;
        public UserRegisterSource RegisterSource { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Domain.Enums.Language Language { get; set; }
        public Theme Theme { get; set; }
        public Profile ProfileType { get; set; }
        public ICollection<UserPermissionEntity> UserPermissions { get; set; }
        public ICollection<UserTruckEntity> UserTrucks { get; set; }
        public ICollection<EventEntity> Events { get; set; }
        public TruckEntity Truck { get; set; }
 
    }
}
