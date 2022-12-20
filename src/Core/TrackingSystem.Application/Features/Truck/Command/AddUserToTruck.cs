using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;

namespace TrackingSystem.Application.Features.Truck.Command
{
	public static class AddUserToTruck
    {

		public sealed record Command(Guid UserId, int TruckId) : IRequest<Unit>;

		public sealed class Handler : IRequestHandler<Command, Unit>
		{
			private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				await _unitOfWork.Trucks.AddUserToTruck(request.UserId, request.TruckId, cancellationToken);

				await _unitOfWork.SaveChangesAsync(cancellationToken);

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
