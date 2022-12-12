using FluentValidation;
using TrackingSystem.Application.Common.Extension;
using TrackingSystem.Application.Common.Factories.EntitiesFactories.User;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Manager;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Services.Interfaces.Permission;

namespace TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Command
{
    public static class Register
    {
        public sealed record Command(string Login, string Email, string Password, string PasswordCopy, string FirstName, string LastName, string PhoneNumber,
                                      Base64File? PhotoFile, Language Language, Profile Profile) : IRequestWrapper<UserEntity>;

        public sealed class Handler : IRequestHandlerWrapper<Command, UserEntity>
        {
            private readonly IUserManager _userManager;
            private readonly ITokenGenerator _tokenGenerator;
            private readonly IPermissionsMapper _permissionsMapper;
            private readonly IFileManager _fileManager;

            public Handler(IUserManager userManager, ITokenGenerator tokenGenerator, IPermissionsMapper permissionsMapper, IFileManager fileManager)
            {
                _userManager = userManager;
                _tokenGenerator = tokenGenerator;
                _permissionsMapper = permissionsMapper;
                _fileManager = fileManager;
            }

            public async Task<UserEntity> Handle(Command request, CancellationToken cancellationToken)
            {
                var isEmailTaken = await _userManager.IsEmailTakenAsync(request.Email, cancellationToken);
                if (isEmailTaken)
                {
                    throw new InvalidRequestException($"Email {request.Email} is already taken");
                }

                string ftpPhoto = String.Empty;
                if (request.PhotoFile != null)
                {
                    ftpPhoto = _fileManager.SaveFile(SaveType.User, Guid.NewGuid().ToString(),request.PhotoFile);
                }

                var newUser = UserEntityFacotry.CreateFromRegisterCommand(request);
                newUser.FilePath = ftpPhoto;
                newUser.Theme = Theme.Light;
                newUser.EmailConfirmed = true;

                newUser.UserPermissions = _permissionsMapper.GetPermissionsByProfile(request.Profile)
                                                   .Select(c => UserPermissionEntityFactory.CreateFromData(c.PermissionDomainName, c.PermissionFlagValue, newUser.Id))
                                                   .ToList();

                var result = await _userManager.RegisterAsync(newUser, request.Password);

                var registeredUser = result.Item1;
                var emailConfirmationToken = await _tokenGenerator.GenerateEmailConfirmationTokenAsync(registeredUser, cancellationToken);


                //BackgroundJob.Enqueue(() => _emailSender.SendEmailConfirmationEmailAsync(registeredUser.Email, emailConfirmationToken, registeredUser.Id, EmailType.ConfirmAccount, cancellationToken));

                foreach(var permission in registeredUser.UserPermissions)
                {
                    permission.User = null;
                };

                return registeredUser;
            }
        }
        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Email).NotEmpty().EmailAddress();
                RuleFor(c => c).Must(c => c.Password == c.PasswordCopy);
                RuleFor(c => c.Password).Matches(RegexExtension.PasswordValidationRegex);
                RuleFor(c => c.PasswordCopy).Matches(RegexExtension.PasswordValidationRegex);
            }
        }
    }
}
