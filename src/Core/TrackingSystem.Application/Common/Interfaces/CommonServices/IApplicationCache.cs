namespace TrackingSystem.Application.Common.Interfaces.CommonServices
{
    public interface IApplicationCache<TCacheObj>
        where TCacheObj : class
    {
        Task RemoveAsync(string key, CancellationToken cancellationToken);
        Task<TCacheObj> GetAysnc(string key, CancellationToken cancellationToken);
        Task InsertAsync(string key, TCacheObj obj, CancellationToken cancellationToken);
    }
}
