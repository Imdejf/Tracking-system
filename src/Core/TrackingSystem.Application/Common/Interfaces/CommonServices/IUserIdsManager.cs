namespace TrackingSystem.Application.Common.Interfaces.CommonServices
{
    public interface IUserIdsManager
    {
        void Add(string connectionId, string userId);
        void RemoveByConnectionId(string connectionId);
        string GetUserIdByConnectionId(string connectionId);
        List<string> GetConnectionIdsByUserId(string userId);
    }
}
