using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Features.User.Dto;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.User.Query
{
    public static class GetUserById
    {

        public sealed record Query(Guid UserId) : IRequest<UserDto>;
        public sealed class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly IUserManager _UserManager;
            public Handler(IUserManager userManager)
            {
                _UserManager = userManager;
            }

            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var userExists = await _UserManager.ExistsAsync(request.UserId, cancellationToken);
                if (!userExists)
                {
                    throw new EntityNotFoundException("User", request.UserId);
                }

                var user = await _UserManager.GetByIdAsync(request.UserId, cancellationToken);
               
                foreach(var entity in user.UserPermissions)
                {
                    entity.User = null;
                }

                return UserDto.CreateFromEntity(user);
            }
        }
    }
}

