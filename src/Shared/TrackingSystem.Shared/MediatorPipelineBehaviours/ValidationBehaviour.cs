using TrackingSystem.Shared.Exceptions;
using FluentValidation;
using MediatR;

namespace TrackingSystem.Shared.MediatorPipelineBehaviours
{
    public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _Validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _Validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_Validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_Validators.Select(c => c.ValidateAsync(validationContext, cancellationToken)));
                if (validationResults.Length > 0 && validationResults.Any(c => !c.IsValid))
                {
                    throw new InvalidRequestException("");
                }
            }
            return await next();
        }
    }
}
