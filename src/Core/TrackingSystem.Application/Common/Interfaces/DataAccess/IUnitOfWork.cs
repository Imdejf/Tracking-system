using TrackingSystem.Application.Common.Interfaces.DataAccess.Repository;
using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Common.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        ITruckRepository Trucks { get; }
        ITruckDetailsRepository TruckDetails { get; }
        IEventRepository Event { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
