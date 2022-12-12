using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Manager;
using TrackingSystem.Domain.Entities.Events;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Application.Features.Event.Command
{
	public static class CreateEvent
    {

		public sealed record Command() : IRequest<Unit>
		{
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

				var newEvent = new EventEntity
				{
					Description = request.Description,
					CreatedDate = DateTime.Now,
					EndDate = request.EndDate,
					CreatedBy = user.Name,
					StartDate = request.StartDate,
					Number = request.Number,
					Title = request.Title,
					UserId = request.UserId,
					EventFiles = new List<EventFileEntity>()
				};

				foreach(var file in request.EventFiles)
				{
					var path = _fileManager.SaveFile(SaveType.EventFile, file.FileName, file.Base64File);
					newEvent.EventFiles.Add(new EventFileEntity
					{
						CreatedBy = user.Name,
						CreatedDate = DateTime.Now,
						FilePath = path,
						Id = Guid.NewGuid()
					});
				}

				await _unitOfWork.Event.AddAsync(newEvent, cancellationToken);
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
