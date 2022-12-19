using FluentValidation;
using MediatR;
using System.IO;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Manager;
using TrackingSystem.Domain.Enums;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Application.Features.User.Comand
{
    public static class UpdateUser
    {

        public sealed record Command(Guid UserId, string FirstName, string LastName, Theme Theme, Base64File? Base64File, bool RemovePhoto) : IRequest<Unit>;

        public sealed class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IUserManager _userManager;
            private readonly IFileManager _fileManager;
            public Handler(IUserManager userManager, IFileManager fileManager)
            {
                _userManager = userManager;
                _fileManager = fileManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.GetByIdAsync(request.UserId, cancellationToken);

                if(user is null)
                {
                    throw new EntityNotFoundException($"User with id: {request.UserId} doesnt exist");
                }

                if (request.RemovePhoto)
                {
                    if (_fileManager.ExistFile(user.FilePath) && !String.IsNullOrEmpty(user.FilePath))
                    {
                        _fileManager.DeleteFile(user.FilePath);
                    }
                    user.FilePath = "";
                }

                if (request.Base64File != null)
                {
                    if (_fileManager.ExistFile(user.FilePath) && !String.IsNullOrEmpty(user.FilePath))
                    {
                        _fileManager.DeleteFile(user.FilePath);
                    }
                    var path = _fileManager.SaveFile(SaveType.User, Guid.NewGuid().ToString(),request.Base64File);
                    user.FilePath = path;
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Name = request.FirstName + " " + request.LastName;
                user.Theme = request.Theme;

                await _userManager.UpdateUserAsync(user, cancellationToken);

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
