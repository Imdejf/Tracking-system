using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;

namespace TrackingSystem.Application.Features.User.Comand
{
    public static class RemoveUser
    {

        public sealed record Command(Guid UserId) : IRequest<Unit>;

        public sealed class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IUserManager _userManager;
            public Handler(IUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.GetByIdAsync(request.UserId, cancellationToken);
                await _userManager.RemoveAccountAsync(user, cancellationToken);

                return Unit.Value;
            }
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.UserId).NotEqual(Guid.Empty);
            }
        }


    }
}
