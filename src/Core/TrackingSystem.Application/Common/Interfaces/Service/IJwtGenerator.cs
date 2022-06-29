using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Models;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess.Service
{
    public interface IJwtGenerator
    {
        JwtGenerationResult Generate(UserDTO user);
    }
}
