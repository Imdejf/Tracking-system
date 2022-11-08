using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.User.Comand
{
    public static class UpdateUser
    {

        public sealed record Command(Guid UserId, string FirstName, string LastName, Theme Theme) : IRequest<Unit>;

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

                if(user is null)
                {
                    throw new EntityNotFoundException($"User with id: {request.UserId} doesnt exist");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Name = request.FirstName + " " + request.LastName;
                user.Theme = request.Theme;

                await _userManager.UpdateUserAsync(user, cancellationToken);

                return Unit.Value;
            }
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {

            }
        }


    }
}
