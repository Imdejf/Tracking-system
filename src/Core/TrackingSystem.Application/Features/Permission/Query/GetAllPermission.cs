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
    public static class GetAllPermission
    {

        public sealed record Query() : IRequestWrapper<IEnumerable<PermissionDTO>>;

        public sealed class Handler : IRequestHandlerWrapper<Query, IEnumerable<PermissionDTO>>
        {
            private readonly IPermissionsMapper _permissionsMapper;
            public Handler(IPermissionsMapper permissionsMapper)
            {
                _permissionsMapper = permissionsMapper;
            }

            public async Task<IEnumerable<PermissionDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var allPermissions = _permissionsMapper
                      .GetPermissionsAsObjects()
                      .Select(c => PermissionDtoFactory.CreateFromData(c.PermissionDomainName, c.PermissionFlagName, c.PermissionFlagValue));
                
                return allPermissions;
            }
        }
    }
}
