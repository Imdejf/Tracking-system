using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Domain.Enums;

namespace TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Models
{
    public sealed class UserRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRegisterSource Source { get; set; }
        public Guid ShopId { get; set; }

        public static UserRegisterModel CreateForExternalRegister(string email, string firstName, string lastName, UserRegisterSource registerSource, Guid shopId)
        {
            return new(email, true, Guid.NewGuid() + "A123!",firstName,lastName, registerSource, shopId);
        }
        public static UserRegisterModel CreateForClassicRegister(string email, string password, string firstName, string lastName, Guid shopId)
        {
            return new(email, false, password,firstName,lastName,UserRegisterSource.Standard,shopId);
        }

        private UserRegisterModel(string email, bool emailConfirmed, string password, string firstName, string lastName, UserRegisterSource registerSource, Guid shopId)
        {
            Email = email;
            EmailConfirmed = emailConfirmed;
            Password = password;
            Source = registerSource;
            FirstName = firstName;
            LastName = lastName;
            ShopId = shopId;
        }
    }
}
