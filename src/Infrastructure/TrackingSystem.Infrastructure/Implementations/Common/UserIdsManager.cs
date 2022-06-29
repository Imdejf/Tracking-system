using TrackingSystem.Application.Common.Interfaces.CommonServices;

namespace TrackingSystem.Infrastructure.Implementations.Common
{
    public sealed class UserIdsManager : IUserIdsManager
    {
        private readonly Dictionary<string, string> _keyValueIds;
        public UserIdsManager()
        {
            _keyValueIds = new Dictionary<string, string>();
        }
        public void Add(string connectionId, string userId)
        {
            _keyValueIds.Add(connectionId, userId);
        }

        public List<string> GetConnectionIdsByUserId(string userId)
        {
            return _keyValueIds.Where(c => c.Value == userId).Select(c => c.Key).ToList();
        }

        public string GetUserIdByConnectionId(string connectionId)
        {
            return _keyValueIds[connectionId];
        }

        public void RemoveByConnectionId(string connectionId)
        {
            _keyValueIds.Remove(connectionId);
        }
    }
}

