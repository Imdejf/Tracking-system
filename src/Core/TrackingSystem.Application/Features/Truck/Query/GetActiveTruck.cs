using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Features.Truck.Query
{
	public static class GetActiveTruck
    {

		public sealed record Query() : IRequest<List<TruckDetailsEntity>>;

		public sealed class Handler : IRequestHandler<Query, List<TruckDetailsEntity>>
		{
			private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<TruckDetailsEntity>> Handle(Query request, CancellationToken cancellationToken)
			{
				var truckList = await _unitOfWork.TruckDetails.GetAllActive(cancellationToken);

				return truckList;
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
