using MediatR;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Features.User.Dto;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.User.Query
{
    public static class GetAllUser
    {
        public sealed record Query(Guid UserId) : IRequest<List<UserDto>>;
        public sealed class Handler : IRequestHandler<Query, List<UserDto>>
        {
            private readonly IUserManager _UserManager;
            public Handler(IUserManager userManager)
            {
                _UserManager = userManager;
            }

            public async Task<List<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userExists = await _UserManager.ExistsAsync(request.UserId, cancellationToken);
                if (!userExists)
                {
                    throw new EntityNotFoundException("User", request.UserId);
                }

                var userList = await _UserManager.GetAlUser(request.UserId, cancellationToken);

                var userDtoList = new List<UserDto>();
                foreach (var user in userList)
                {
                    foreach(var permission in user.UserPermissions)
                    {
                        permission.User = null;
                    }

                    userDtoList.Add(UserDto.CreateFromEntity(user));
                }
                return userDtoList;
            }
        }
    }
}