using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess.Repository
{
    public interface ITruckDetailsRepository : IBaseRepository<TruckDetailsEntity>
    {
        Task<TruckDetailsEntity> GetById(int truckId, CancellationToken cancellationToken);
        void AttachedTruckDetails(Guid truckId);
        Task<List<TruckDetailsEntity>> GetAllActive(CancellationToken cancellationToken);
    }
}
