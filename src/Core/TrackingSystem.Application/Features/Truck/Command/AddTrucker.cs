using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Repository;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.Truck.Command
{
	public static class AddTrucker
    {

		public sealed record Command(Guid UserId, int TruckId) : IRequest<Unit>;

		public sealed class Handler : IRequestHandler<Command, Unit>
		{
			private readonly IUnitOfWork _unitOfWork;
			private readonly IUserManager _userManager;
            public Handler(IUnitOfWork unitOfWork, IUserManager userManager)
            {
                _unitOfWork = unitOfWork;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				var userExist = await _userManager.GetByIdAsync(request.UserId, cancellationToken);

				if(userExist is null) {
					throw new EntityNotFoundException($"User with id {request.UserId} doesnt exist");
				}

				var truck = await _unitOfWork.Trucks.GetTruckById(request.TruckId, cancellationToken);

				truck.UserId = request.UserId;

				try
				{
				await _unitOfWork.SaveChangesAsync();

				}
				catch(Exception ex)
				{
					throw new Exception(ex.InnerException.Message);
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
