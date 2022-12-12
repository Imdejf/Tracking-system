using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Features.Truck.Query.Dto;
using TrackingSystem.Domain.Entities.Truck;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.Truck.Query
{
	public static class GetTruckByUserId
    {

		public sealed record Query(Guid UserId) : IRequest<List<TruckDto>>;

		public sealed class Handler : IRequestHandler<Query, List<TruckDto>>
		{
			private readonly IUnitOfWork _unitOfWork;
			private readonly IUserManager _userManager;
            public Handler(IUnitOfWork unitOfWork, IUserManager userManager)
            {
                _unitOfWork = unitOfWork;
                _userManager = userManager;
            }

            public async Task<List<TruckDto>> Handle(Query request, CancellationToken cancellationToken)
			{
				var user = await _userManager.GetByIdAsync(request.UserId, cancellationToken);
				if(user is null)
				{
					throw new EntityNotFoundException($"User with id {request.UserId} doesnt exist");
				}


                if ((int)user.ProfileType > 1)
				{
                     var truckList = await _unitOfWork.Trucks.GetFullyTruckAsync(cancellationToken);
                    return truckList.Select(c => TruckDto.CreateFromEntity(c)).ToList();
                }
                else
				{
                    var truckList  = await _unitOfWork.Trucks.GetTruckByUserIdAsync(request.UserId, cancellationToken);
                    return truckList.Select(c => TruckDto.CreateFromEntity(c.Truck)).ToList();
                }
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
