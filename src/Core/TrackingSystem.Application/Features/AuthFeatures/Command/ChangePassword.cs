using FluentValidation;
using TrackingSystem.Application.Common.Exceptions;
using TrackingSystem.Application.Common.Extension;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Shared.Exceptions;
using MediatR;

namespace TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Command
{
    public static class ChangePassword
    {
        public sealed record Command(Guid UserId, string OldPassword, string NewPassword, string NewPasswordCopy) : IRequestWrapper<Unit>;
        public sealed class Handler : IRequestHandlerWrapper<Command,Unit>
        {
            private readonly IUserManager _UserManager;
            public Handler(IUserManager userManager)
            {
                _UserManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _UserManager.GetByIdAsync(request.UserId, cancellationToken);
                if (currentUser is null)
                {
                    throw new EntityNotFoundException($"User with Id : {request.UserId} doesn`t exists");
                }

                var result = await _UserManager.ChangePasswordAsync(currentUser, request.OldPassword, request.NewPassword, cancellationToken);
                if (!result.IsSuccessful)
                {
                    throw new IdentityException(result);
                }

                return Unit.Value;
            }
        }
        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c).Must(c => c.NewPassword == c.NewPasswordCopy);
                RuleFor(c => c.UserId).NotEqual(Guid.Empty);
                RuleFor(c => c.OldPassword).Matches(RegexExtension.PasswordValidationRegex);
                RuleFor(c => c.NewPassword).Matches(RegexExtension.PasswordValidationRegex);
                RuleFor(c => c.NewPasswordCopy).Matches(RegexExtension.PasswordValidationRegex);
            }
        }
    }
}
