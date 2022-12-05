using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess.Repository
{
    public interface ITruckRepository : IBaseRepository<TruckEntity>
    {
        Task<List<UserTruckEntity>> GetTruckByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<TruckEntity>> GetFullyTruckAsync(CancellationToken cancellationToken);
        Task<TruckEntity> GetTruckById(int truckId, CancellationToken cancellationToken);
        Task AddUserToTruck(Guid userId, int truckId, CancellationToken cancellationToken);
        void RemoveUserFromTruck(Guid userId, int truckId);
    }
}
