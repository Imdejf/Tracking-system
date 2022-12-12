using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Manager;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Application.Features.Event.Command
{
    public static class UpdateEvent
    {

		public sealed record Command() : IRequest<Unit>
        {
            public Guid EventId { get; set; }
            public Guid UserId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Number { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public EventType EventType { get; set; }
            public ICollection<EventFile>? EventFiles { get; set; }
            public record EventFile
            {
                public string FileName { get; set; }
                public Base64File Base64File { get; set; }
            }
        }

		public sealed class Handler : IRequestHandler<Command, Unit>
		{
            private readonly IUnitOfWork _unitOfWork;
            private readonly IFileManager _fileManager;
            private readonly IUserManager _userManager;

            public Handler(IUnitOfWork unitOfWork, IFileManager fileManager, IUserManager userManager)
            {
                _unitOfWork = unitOfWork;
                _fileManager = fileManager;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
			{
                var user = await _userManager.GetByIdAsync(request.UserId, cancellationToken);

                if (user is null)
                {
                    throw new EntityNotFoundException($"User with id {request.UserId} doesnt exist");
                }

                var currentEvent = await _unitOfWork.Event.GetFullyEventByIdAsync(request.EventId, cancellationToken);

                if (currentEvent is null)
                {
                    throw new EntityNotFoundException($"Event with id {request.UserId} doesnt exist");
                }

                currentEvent.StartDate = request.StartDate;
                currentEvent.EndDate = request.EndDate;
                currentEvent.Description = request.Description;
                currentEvent.EventType = request.EventType;
                currentEvent.LastModifiedDate = DateTime.Now;
                currentEvent.Title = request.Title;
                currentEvent.Number = request.Number;
                currentEvent.LastModifiedBy = user.Name;

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
