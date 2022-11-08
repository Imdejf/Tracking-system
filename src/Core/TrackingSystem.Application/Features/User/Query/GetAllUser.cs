using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.User.Query
{
    public static class GetAllUser
    {
        public sealed record Query(Guid UserId) : IRequest<List<UserEntity>>;
        public sealed class Handler : IRequestHandler<Query, List<UserEntity>>
        {
            private readonly IUserManager _UserManager;
            public Handler(IUserManager userManager)
            {
                _UserManager = userManager;
            }

            public async Task<List<UserEntity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userExists = await _UserManager.ExistsAsync(request.UserId, cancellationToken);
                if (!userExists)
                {
                    throw new EntityNotFoundException("User", request.UserId);
                }

                var userList = await _UserManager.GetAlUser(request.UserId, cancellationToken);

                foreach (var user in userList)
                {
                    foreach(var permission in user.UserPermissions)
                    {
                        permission.User = null;
                    }
                }
                return userList;
            }
        }
    }
}