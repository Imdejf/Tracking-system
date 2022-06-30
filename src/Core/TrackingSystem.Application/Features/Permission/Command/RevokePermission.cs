using FluentValidation;
using MediatR;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Common.Interfaces.Service;
using TrackingSystem.Shared.Services.Interfaces.Permission;

namespace TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Command
{
    public static class RevokePermission
    {

        public sealed record Command(Guid UserId, string PermissionDomainName, int PermissionFlagValue) : IRequestWrapper<Unit>;

        public sealed class Handler : IRequestHandlerWrapper<Command, Unit>
        {
            private readonly IUserManager _userManager;
            private readonly IUserPermissionManager _userPermission;

            public Handler(IUserManager userManager, IUserPermissionManager userPermission)
            {
                _userManager = userManager;
                _userPermission = userPermission;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var hasPermission = await _userPermission.UserHasPermissionAsync(request.UserId, request.PermissionDomainName, request.PermissionFlagValue, cancellationToken);
                if (hasPermission)
                {
                    await _userPermission.RemovePermissionAsync(request.UserId, request.PermissionDomainName, request.PermissionFlagValue, cancellationToken);
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
