using FluentValidation;
using TrackingSystem.Application.Common.Exceptions;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Shared.Exceptions;
using MediatR;

namespace TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Command
{
    public static class ConfirmEmail
    {
        public sealed record Command(Guid UserId, string EmailConfirmationToken) : IRequestWrapper<Unit>;
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

                var result = await _UserManager.ConfirmEmailAsync(currentUser, request.EmailConfirmationToken, cancellationToken);
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
                RuleFor(c => c.UserId).NotEqual(Guid.Empty);
                RuleFor(c => c.EmailConfirmationToken).NotEmpty();
            }
        }
    }
}
