using FluentValidation;
using TrackingSystem.Application.Common.DTOs;
using TrackingSystem.Application.Common.Factories.DtoFactories;
using TrackingSystem.Application.Common.Interfaces;
using TrackingSystem.Shared.Services.Interfaces.Permission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Query
{
    public static class GetAllProfile
    {

        public sealed record Query() : IRequestWrapper<IEnumerable<ProfileDTO>>;

        public sealed class Handler : IRequestHandlerWrapper<Query, IEnumerable<ProfileDTO>>
        {
            private readonly IPermissionsMapper _permissionsMapper;
            public Handler(IPermissionsMapper permissionsMapper)
            {
                _permissionsMapper = permissionsMapper;
            }

            public async Task<IEnumerable<ProfileDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _permissionsMapper.GetProfilesAsObjects().Select(c => ProfileDtoFactory.CreateFromData(c.FlagName, c.FlagValue));
            }
        }
    }
}
