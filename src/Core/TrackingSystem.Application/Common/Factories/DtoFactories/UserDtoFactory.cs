using System.Linq.Expressions;
using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Application.Common.Factories.DtoFactories
{
    public static class UserDtoFactory
    {
        public static UserDTO CreateFromEntity(UserEntity entity)
        {
            return new UserDTO
            {
                UserName = entity.UserName,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Id = entity.Id,
                Name = entity.Name,
                FilePath = entity.FilePath,
                PhoneNumber = entity.PhoneNumber,
                Profile = entity.ProfileType,
                Permissions = entity.UserPermissions?.GroupBy(c => c.PermissionDomainName).ToDictionary(c => c.Key, c => c.Select(a => a.PermissionFlagValue)),
            };
        }
        public static UserDTO CreateFromData(
            string userName,
            string email,
            string firstName,
            string lastName,
            Guid id,
            string phoneNumber,
            ICollection<UserPermissionEntity> userPermissions)
        {
            return new UserDTO
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Id = id,
                PhoneNumber = phoneNumber,
                Permissions = userPermissions?.GroupBy(c => c.PermissionDomainName).ToDictionary(c => c.Key, c => c.Select(a => a.PermissionFlagValue))
            };
        }

        public static Expression<Func<UserEntity, UserDTO>> DtoFromDomainModelSelector => c =>
            CreateFromData(c.UserName, c.Email, c.FirstName, c.LastName, c.Id, c.PhoneNumber, c.UserPermissions);
    }
}