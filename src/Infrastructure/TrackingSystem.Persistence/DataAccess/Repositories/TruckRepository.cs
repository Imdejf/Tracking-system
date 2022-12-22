using Microsoft.EntityFrameworkCore;
using TrackingSystem.Application.Common.Interfaces.DataAccess.Repository;
using TrackingSystem.Domain.Entities.Truck;

namespace JustCommerce.Persistence.DataAccess.Repositories
{
    internal sealed class TruckRepository : BaseRepository<TruckEntity>, ITruckRepository
    {
        private DbSet<UserTruckEntity> _userTrucks;
        public TruckRepository(DbSet<TruckEntity> entities, DbSet<UserTruckEntity> userTrucks) : base(entities)
        {
            _userTrucks = userTrucks;
        }

        public async Task AddUserToTruck(Guid userId, int truckId, CancellationToken cancellationToken)
        {
            var userTruck = new UserTruckEntity
            {
                TruckId = truckId,
                UserId = userId
            };

            await _userTrucks.AddAsync(userTruck, cancellationToken);
        }

        public Task<List<TruckEntity>> GetFullyTruckAsync(CancellationToken cancellationToken)
        {
            return _Entities.Include(c => c.UserTrucks)
                            .ThenInclude(c => c.User)
                            .Include(c => c.TruckDetails)
                            .Include(c => c.User)
                            .ToListAsync(cancellationToken);
        }

        public Task<TruckEntity> GetTruckById(int truckId, CancellationToken cancellationToken)
        {
            return _Entities.Where(c => c.TruckId == truckId)
                            .Include(c => c.TruckDetails)
                            .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<UserTruckEntity>> GetTruckByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _userTrucks.Where(c => c.UserId == userId)
                              .Include(c => c.Truck)
                              .ThenInclude(c => c.TruckDetails)
                              .Include(c => c.Truck)
                              .ThenInclude(c => c.User)
                              .ToListAsync(cancellationToken);
        }

        public void RemoveUserFromTruck(Guid userId, int truckId)
        {
            var userTruck = new UserTruckEntity
            {
                TruckId = truckId,
                UserId = userId
            };

            _userTrucks.Remove(userTruck);
        }
    }
}
