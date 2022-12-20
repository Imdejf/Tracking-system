using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackingSystem.Application.Common.Interfaces.DataAccess;

namespace TrackingSystem.Application.Features.Truck.Command
{
    public static class RemoveTrucker
    {

		public sealed record Command(int TruckId) : IRequest<Unit>;

		public sealed class Handler : IRequestHandler<Command, Unit>
		{
			private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
				var truck = await _unitOfWork.Trucks.GetTruckById(request.TruckId, cancellationToken);

				truck.UserId = null;

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
