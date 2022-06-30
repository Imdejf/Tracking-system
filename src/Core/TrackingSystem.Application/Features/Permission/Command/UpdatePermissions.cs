using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Service;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Services.Interfaces.Permission;

namespace TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Command
{
    public static class UpdatePermissions
    {
        public sealed record Command(Guid UserId, IEnumerable<PermissionDTO> PermissionsToGrant, IEnumerable<PermissionDTO> PermissionsToRevoke) : IRequestWrapper<Unit>;


        public sealed class Handler : IRequestHandlerWrapper<Command, Unit>
        {
            private readonly IUserManager _userManager;
            private readonly IUserPermissionManager _userPermission;
            private readonly IPermissionsMapper _permissionsMapper;
            public Handler()
            {
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var userExist = await _userManager.ExistsAsync(request.UserId, cancellationToken);

                if (!userExist)
                {
                    throw new EntityNotFoundException($"UserEntity with Id : {request.UserId} doesn`t exists");
                }

                await checkIfHasAllPermissions(request.PermissionsToRevoke, request.UserId, cancellationToken);
                await checkIfHasNoneOfPermissions(request.PermissionsToGrant, request.UserId, cancellationToken);
                await _userPermission.RemovePermissionRangeAsync(request.PermissionsToRevoke, request.UserId, cancellationToken);
                await _userPermission.GrantPermissionRangeAsync(request.PermissionsToGrant, request.UserId, cancellationToken);

                return Unit.Value;

            }

            private async Task<bool> checkIfHasAllPermissions(IEnumerable<PermissionDTO> permissions, Guid userId, CancellationToken cancellationToken)
            {
                foreach (var permission in permissions)
                {
                    var hasPermissions = await _userPermission.UserHasPermissionAsync(userId, permission.PermissionDomainName, permission.PermissionFlagValue, cancellationToken);
                    if (!hasPermissions)
                    {
                        throw new InvalidDataException($"User wieth Id : {userId} doesn`t have permission {permission.PermissionDomainName} {permission.PermissionFlagValue}");
                    }
                }
                return true;
            }
            private async Task<bool> checkIfHasNoneOfPermissions(IEnumerable<PermissionDTO> permissions, Guid userId, CancellationToken cancellationToken)
            {
                foreach (var permission in permissions)
                {
                    var hasPermissions = await _userPermission.UserHasPermissionAsync(userId, permission.PermissionDomainName, permission.PermissionFlagValue, cancellationToken);
                    if (hasPermissions)
                    {
                        throw new InvalidDataException($"User wieth Id : {userId} doesn`t have permission {permission.PermissionDomainName} {permission.PermissionFlagValue}");
                    }
                }
                return true;
            }
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator(IPermissionValidator permissionValidator)
            {
                RuleFor(c => c.UserId)
                    .NotEqual(Guid.Empty);

                RuleFor(c => c.PermissionsToGrant)
                    .NotNull();

                RuleFor(c => c.PermissionsToRevoke)
                    .NotNull();

                RuleForEach(c => c.PermissionsToGrant)
                    .NotNull()
                    .Must(c => permissionValidator.IsPermissionValid(c.PermissionDomainName, c.PermissionFlagValue))
                    .WithMessage((a, b) => $"Permission {b.PermissionDomainName} {b.PermissionFlagValue} doesn`t exists");

                RuleForEach(c => c.PermissionsToRevoke)
                    .NotNull()
                    .Must(c => permissionValidator.IsPermissionValid(c.PermissionDomainName, c.PermissionFlagValue))
                    .WithMessage((a, b) => $"Permission {b.PermissionDomainName} {b.PermissionFlagValue} doesn`t exists");

            }
        }

    }
}
