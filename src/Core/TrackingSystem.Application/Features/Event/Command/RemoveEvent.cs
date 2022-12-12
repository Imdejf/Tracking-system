using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackingSystem.Application.Common.Interfaces.DataAccess;

namespace TrackingSystem.Application.Features.Event.Command
{
    public static class RemoveEvent
    {

		public sealed record Command(Guid EventId) : IRequest<Unit>;

		public sealed class Handler : IRequestHandler<Command, Unit>
		{
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				_unitOfWork.Event.RemoveById(request.EventId);

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
