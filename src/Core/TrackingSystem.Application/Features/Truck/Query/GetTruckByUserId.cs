using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Features.Truck.Query.Dto;

namespace TrackingSystem.Application.Features.Truck.Query
{
	public static class GetTruckByUserId
    {

		public sealed record Query(Guid UserId) : IRequest<List<TruckDto>>;

		public sealed class Handler : IRequestHandler<Query, List<TruckDto>>
		{
			private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<TruckDto>> Handle(Query request, CancellationToken cancellationToken)
			{
				var truckList = await _unitOfWork.Trucks.GetTruckByUserIdAsync(request.UserId, cancellationToken);
				return truckList.Select(c => TruckDto.CreateFromEntity(c.Truck)).ToList();
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
