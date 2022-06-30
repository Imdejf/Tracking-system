namespace TrackingSystem.Application.Common.Interfaces.DataAccess
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
