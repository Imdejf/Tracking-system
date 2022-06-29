using TrackingSystem.Shared.Attributes;
using TrackingSystem.Shared.Services.Interfaces.Permission;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace TrackingSystem.Shared.MediatorPipelineBehaviours
{
    internal class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IPermissionValidator _PermissionsValidator;
        private HttpContext _HttpContext => _HttpContextAccessor.HttpContext;
        public AuthorizationBehaviour(IHttpContextAccessor httpContextAccessor, IPermissionValidator permissionsValidator)
        {
            _HttpContextAccessor = httpContextAccessor;
            _PermissionsValidator = permissionsValidator;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            VerifyPermissionsAttribute[] validationAttributes = Attribute
                .GetCustomAttributes(typeof(TRequest))
                .Where(c => c is VerifyPermissionsAttribute)
                .Select(c => c as VerifyPermissionsAttribute)
                .ToArray();
            if (validationAttributes.Length == 0)
            {
                return await next();
            }

            try
            {
                var token = _HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

                bool[] validationResults = validationAttributes
                    .Select(c => _PermissionsValidator.HasPermissions(c._PermissionDomainEnumType, c._RequiredPermissions, token, c._Method))
                    .ToArray();

                if (validationResults.Any(c => !c))
                {
                    throw new UnauthorizedAccessException();
                }

                return await next();
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
