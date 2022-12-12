using Microsoft.EntityFrameworkCore;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Repository;
using TrackingSystem.Domain.Entities.Events;

namespace JustCommerce.Persistence.DataAccess.Repositories
{
    internal class EventRepository : BaseRepository<EventEntity>, IEventRepository
    {
        public EventRepository(DbSet<EventEntity> entities) : base(entities)
        {
        }

        public Task<List<EventEntity>> GetFullyEventAsync(CancellationToken cancellationToken)
        {
            return _Entities.Include(c => c.EventFiles).ToListAsync(cancellationToken);
        }

        public Task<EventEntity> GetFullyEventByIdAsync(Guid eventId, CancellationToken cancellationToken)
        {
            return _Entities.Where(c => c.Id == eventId).Include(c => c.EventFiles).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
