using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Domain.Entities.Events;

namespace TrackingSystem.Application.Features.Event.Query
{
	public static class GetAllEvent
    {

		public sealed record Query() : IRequest<List<EventEntity>>;

		public sealed class Handler : IRequestHandler<Query, List<EventEntity>>
		{
			private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<EventEntity>> Handle(Query request, CancellationToken cancellationToken)
			{
				var allEvent = await _unitOfWork.Event.GetFullyEventAsync(cancellationToken);

				return allEvent;
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
