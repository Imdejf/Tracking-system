using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.AuthFeatures.Command
{
	public static class ChangePasswordByAdmin
    {

		public sealed record Command(Guid UserId, string Password, string PasswordCopy) : IRequest<Unit>;

		public sealed class Handler : IRequestHandler<Command, Unit>
		{
            private readonly IUserManager _UserManager;
            public Handler(IUserManager userManager)
            {
                _UserManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				var user = await _UserManager.GetByIdAsync(request.UserId, cancellationToken);

				if(user is null)
				{
					throw new EntityNotFoundException($"User with id {request.UserId} doesnt exist");
				}

                var result = await _UserManager.ChangePasswordByAdminAsync(user, request.Password, cancellationToken);
                if (!result.IsSuccessful)
                {
                    throw new InvalidRequestException(String.Join(',', result.Errors));
                }

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
