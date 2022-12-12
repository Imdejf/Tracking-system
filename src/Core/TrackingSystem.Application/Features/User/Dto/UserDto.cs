using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Domain.Entities.Truck;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Enums;

namespace TrackingSystem.Application.Features.User.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public UserRegisterSource RegisterSource { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Domain.Enums.Language Language { get; set; }
        public Theme Theme { get; set; }
        public string ProfileType { get; set; }
        public ICollection<UserPermissionEntity> UserPermissions { get; set; }
        public ICollection<UserTruckEntity> UserTrucks { get; set; }

        public static UserDto CreateFromEntity(UserEntity entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedDate = entity.CreatedDate,
                LastModifiedDate = entity.LastModifiedDate,
                FilePath = entity.FilePath,
                FirstName = entity.FirstName,
                Language = entity.Language,
                LastName = entity.LastName,
                ProfileType = entity.ProfileType.ToString(),
                RegisterSource = entity.RegisterSource,
                Theme = entity.Theme,
                UserTrucks = entity.UserTrucks,
                UserPermissions = entity.UserPermissions,
            };
        }
    }
}
