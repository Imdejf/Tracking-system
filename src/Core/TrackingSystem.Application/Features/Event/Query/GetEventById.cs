using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Domain.Entities.Events;

namespace TrackingSystem.Application.Features.Event.Query
{
    public static class GetEventById
    {

		public sealed record Query(Guid EventId) : IRequest<EventEntity>;

		public sealed class Handler : IRequestHandler<Query, EventEntity>
		{
			private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<EventEntity> Handle(Query request, CancellationToken cancellationToken)
			{
				var eventEntity = await _unitOfWork.Event.GetFullyEventByIdAsync(request.EventId, cancellationToken);

				return eventEntity;

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
