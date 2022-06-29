using FluentValidation;
using TrackingSystem.Application.Common.Exceptions;
using TrackingSystem.Application.Common.Extension;
using TrackingSystem.Application.Common.Factories.DtoFactories;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Models;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Query
{
    public static class Login
    {
        public sealed record Query(string EmailOrName, string Password) : IRequestWrapper<JwtGenerationResult>;
        public sealed class Handler : IRequestHandlerWrapper<Query, JwtGenerationResult>
        {
            private readonly IUserManager _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Handler(IUserManager userManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<JwtGenerationResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUser = await _userManager.GetUserByMailOrNameAsync(request.EmailOrName, cancellationToken);
                if (currentUser is null)
                {
                    throw new EntityNotFoundException($"User with Email {request.EmailOrName} is not registered");
                }

                if (currentUser.RegisterSource != Domain.Enums.UserRegisterSource.Standard)
                {
                    throw new IdentityException("Invalid login source.");
                }

                var loginResult = await _userManager.LoginAsync(currentUser, request.Password, cancellationToken);
                if (!loginResult.IsSuccessful)
                {
                    if (loginResult.Errors.Length > 0)
                        throw new IdentityException(loginResult);
                    else
                        throw new IdentityException("Passed credentails are invalid");
                }

                return _jwtGenerator.Generate(UserDtoFactory.CreateFromEntity(currentUser));
            }
        }
        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(c => c.EmailOrName).NotEmpty().EmailAddress();
                RuleFor(c => c.Password).Matches(RegexExtension.PasswordValidationRegex);
            }
        }
    }
}
