using FluentValidation;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Service;
using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Services.Interfaces.Permission;
using MediatR;

namespace TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Command
{
    public static class AssignToProfile
    {
        public sealed record Command(Guid UserId, Profile Profile) : IRequestWrapper<Unit>;

        public sealed class Handler : IRequestHandlerWrapper<Command, Unit>
        {
            private readonly IUserManager _userManager;
            private readonly IUserPermissionManager _userPermission;
            private readonly IPermissionsMapper _permissionsMapper;

            public Handler(IUserManager userManager, IUserPermissionManager userPermission, IPermissionsMapper permissionsMapper)
            {
                _userManager = userManager;
                _userPermission = userPermission;
                _permissionsMapper = permissionsMapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var userExist = await _userManager.ExistsAsync(request.UserId, cancellationToken);

                if (!userExist)
                {
                    throw new EntityNotFoundException($"UserEntity with Id : {request.UserId} doesn`t exists");
                }

                await _userPermission.RemoveAllUserPermissions(request.UserId, cancellationToken);

                var thisProfilePermissions = _permissionsMapper
                    .GetPermissionsByProfile(request.Profile)
                    .Select(c => (c.PermissionDomainName, c.PermissionFlagValue));

                await _userPermission.GrantUserPermissions(thisProfilePermissions, request.UserId, cancellationToken);

                return Unit.Value;
            }

        }
        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.UserId)
                    .NotEqual(Guid.Empty);

                RuleFor(c => c.Profile)
                    .IsInEnum();
            }
        }
    }


}
