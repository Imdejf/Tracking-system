using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;

namespace TrackingSystem.Application.Features.Truck.Command
{
	public static class RemoveUserFromTruck
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
				 _unitOfWork.Trucks.RemoveUserFromTruck(request.UserId, request.TruckId);

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
