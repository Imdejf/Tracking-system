using TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Command;
using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Application.Common.Factories.EntitiesFactories.User
{
    public static class UserEntityFacotry
    {
        public static UserEntity CreateFromRegisterCommand(Register.Command command)
        {
            return new UserEntity
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
                EmailConfirmed = false,
                RegisterSource = command.RegisterSource,
                UserName = command.Login,
                CreatedDate = DateTime.Now,
            };
        }
    }
}
