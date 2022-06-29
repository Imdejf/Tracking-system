using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace TrackingSystem.Shared.MediatorPipelineBehaviours
{
    public sealed class TrackingDisposeBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private DbContext _Context;

        public TrackingDisposeBehaviour(DbContext context)
        {
            _Context = context;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _Context.ChangeTracker.Clear();
            return Task.CompletedTask;
        }
    }
}
