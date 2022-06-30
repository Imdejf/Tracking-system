using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Models;

namespace TrackingSystem.Application.Common.Factories.ApplicationModelsFactories
{
    public static class JwtClaimsFactory
    {
        public static JustCommerceJwtClaims CreateFromIntranetUserDTO(UserDTO dto)
        {
            return new JustCommerceJwtClaims
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                PermissionsList = dto.Permissions,
                Id = dto.Id
            };
        }
    }
}
