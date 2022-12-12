using Microsoft.EntityFrameworkCore;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Repository;
using TrackingSystem.Domain.Entities.Truck;

namespace JustCommerce.Persistence.DataAccess.Repositories
{
    internal class TruckDetailsRepository : BaseRepository<TruckDetailsEntity>, ITruckDetailsRepository
    {
        public TruckDetailsRepository(DbSet<TruckDetailsEntity> entities) : base(entities)
        {
        }

        public Task<TruckDetailsEntity> GetById(int truckId, CancellationToken cancellationToken)
        {
            return _Entities.Where(c => c.TruckId == truckId).FirstAsync(cancellationToken);
        }

        public void AttachedTruckDetails(Guid truckId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TruckDetailsEntity>> GetAllActive(CancellationToken cancellationToken) 
        {
            return _Entities.Where(c => c.IgnitionState == true).Include(c => c.Truck).ToListAsync(cancellationToken);
        }
    }
}
