using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Application.Features.User.Query
{
    public static class GetUserById
    {

        public sealed record Query(Guid UserId) : IRequest<UserEntity>;
        public sealed class Handler : IRequestHandler<Query, UserEntity>
        {
            private readonly IUserManager _UserManager;
            public Handler(IUserManager userManager)
            {
                _UserManager = userManager;
            }

            public async Task<UserEntity> Handle(Query request, CancellationToken cancellationToken)
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

                return user;
            }
        }
    }
}

