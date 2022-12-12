using TrackingSystem.Domain.Entities.Events;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess.Repository
{
    public interface IEventRepository : IBaseRepository<EventEntity>
    {
        Task<EventEntity> GetFullyEventByIdAsync(Guid eventId, CancellationToken cancellationToken);
        Task<List<EventEntity>> GetFullyEventAsync(CancellationToken cancellationToken);
    }
}
