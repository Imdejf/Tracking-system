using TrackingSystem.Application.Common.Interfaces.CommonServices;
using TrackingSystem.Shared.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace TrackingSystem.Infrastructure.Implementations.Common
{
    public sealed class ApplicationCache<TCacheObj> : IApplicationCache<TCacheObj>
          where TCacheObj : class
    {
        private readonly IDistributedCache _distributedCache;

        public ApplicationCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<TCacheObj> GetAysnc(string key, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidRequestException("Key can not be empty or null");
            }

            var jsonObj = await _distributedCache.GetStringAsync(key, cancellationToken);
            return JsonSerializer.Deserialize<TCacheObj>(jsonObj);
        }

        public Task InsertAsync(string key, TCacheObj obj, CancellationToken cancellationToken)
        {
            if (obj == null)
            {
                throw new InvalidRequestException("Cache obj can not be null");
            }
            return _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(obj), cancellationToken);
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidRequestException("Key can not be empty or null");
            }

            return _distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
