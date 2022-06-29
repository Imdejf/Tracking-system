using TrackingSystem.Shared.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Shared.MediatorPipelineBehaviours
{
    public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILoggingTool _Logger;
        private readonly ICurrentUserService _CurrentUserService;
        public LoggingBehaviour(ILoggingTool logger, ICurrentUserService currentUserService)
        {
            _Logger = logger;
            _CurrentUserService = currentUserService;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_CurrentUserService.CurrentUser != null)
            {
                _Logger.LogInformation($"Mediator | {request.GetType().Name} | {_CurrentUserService.CurrentUser.Email}");
            }
            else
            {
                _Logger.LogInformation($"Mediator | {request.GetType().Name} | Annonymous User");
            }
            return next();
        }
    }
}
