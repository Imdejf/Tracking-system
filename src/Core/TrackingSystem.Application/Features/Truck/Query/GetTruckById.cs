using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Features.Truck.Query
{
	public sealed class GetTruckById
    {

		public sealed record Query(int TruckId) : IRequest<TruckEntity>;

		public sealed class Handler : IRequestHandler<Query, TruckEntity>
		{
			private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<TruckEntity> Handle(Query request, CancellationToken cancellationToken)
			{
				var truck = await _unitOfWork.Trucks.GetTruckById(request.TruckId, cancellationToken);

				return truck;
			}
		}

		public sealed class Validator : AbstractValidator<Query>
		{
			public Validator()
			{

			}
		}


	}
}
