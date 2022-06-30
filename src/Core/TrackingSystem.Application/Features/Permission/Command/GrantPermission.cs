using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Factories.EntitiesFactories.User;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Service;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Services.Interfaces.Permission;

namespace TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Command
{
    public static class GrantPermission
    {

        public sealed record Command(Guid UserId, string PermissionDomainName, int PermissionFlagValue) : IRequestWrapper<Unit>;

        public sealed class Handler : IRequestHandlerWrapper<Command, Unit>
        {
            private readonly IUserManager _userManager;
            private readonly IUserPermissionManager _userPermission;
            public Handler(IUserPermissionManager userPermission, IUserManager userManager)
            {
                _userPermission = userPermission;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var userExist = await _userManager.ExistsAsync(request.UserId, cancellationToken);

                if (!userExist)
                {
                    throw new EntityNotFoundException($"User with Id : {request.UserId} doesn`t exists");
                }
                var hasPermission = await _userPermission.UserHasPermissionAsync(request.UserId, request.PermissionDomainName, request.PermissionFlagValue, cancellationToken);
                if (!hasPermission)
                {
                    await _userPermission.AddPermissionAsync(
                        UserPermissionEntityFactory.CreateFromData(request.PermissionDomainName, request.PermissionFlagValue, request.UserId)
                   );
                }

                return Unit.Value;
            }
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator(IPermissionValidator permissionValidator)
            {
                RuleFor(c => c.UserId)
                    .NotEqual(Guid.Empty);

                RuleFor(c => new Tuple<string, int>(c.PermissionDomainName, c.PermissionFlagValue))
                    .Must(c => permissionValidator.IsPermissionValid(c.Item1, c.Item2))
                    .WithErrorCode("InvalidPermission")
                    .WithMessage((a, b) => $"Permission {b.Item1} {b.Item2} doesn`t exists");
            }
        }

    }
}
