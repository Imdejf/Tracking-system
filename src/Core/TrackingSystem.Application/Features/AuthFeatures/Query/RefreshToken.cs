using TrackingSystem.Application.Common.Exceptions;
using TrackingSystem.Application.Common.Factories.DtoFactories;
using TrackingSystem.Application.Common.Factories.EntitiesFactories;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Service;
using TrackingSystem.Application.Models;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Services.Interfaces;
using MediatR;

namespace TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Query
{
    public static class RefreshToken
    {
        public sealed record Query() : IRequest<JwtGenerationResult>;
        public sealed class Handler : IRequestHandler<Query, JwtGenerationResult>
        {
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserManager _userManager;
            private readonly ICurrentUserService _currentUserService;
            public Handler(IJwtGenerator jwtGenerator, IUserManager userManager, ICurrentUserService currentUserService)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _currentUserService = currentUserService;
            }

            public async Task<JwtGenerationResult> Handle(Query request, CancellationToken cancellationToken)
            {
                if (_currentUserService.CurrentUser.Id == Guid.Empty)
                {
                    throw new IdentityException("User isn`t loged in");
                }
                var currentUser = await _userManager.GetByIdAsync(_currentUserService.CurrentUser.Id, cancellationToken);
                if (currentUser is null)
                {
                    throw new EntityNotFoundException($"User with Id {_currentUserService.CurrentUser.Id} doesn`t exists");
                }

                return _jwtGenerator.Generate(UserDtoFactory.CreateFromEntity(currentUser));
            }
        }
    }
}
