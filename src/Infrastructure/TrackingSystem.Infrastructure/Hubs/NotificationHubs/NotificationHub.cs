using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TrackingSystem.Infrastructure.Hubs.NotificationHubs
{
    [Authorize]
    public sealed class NotificationHub : Hub
    {
        private readonly IUserIdsManager _userIdsManager;
        public NotificationHub(IUserIdsManager userIdsManager)
        {
            _userIdsManager = userIdsManager;
        }

        public override async Task OnConnectedAsync()
        {
            _userIdsManager.Add(Context.ConnectionId, Context.UserIdentifier);
            await Clients.User(Context.UserIdentifier).SendAsync("ReciveMessage", "User connected");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _userIdsManager.RemoveByConnectionId(Context.ConnectionId);
            await Clients.User(Context.UserIdentifier).SendAsync("ReciveMessage", "User disconnected");

            await base.OnDisconnectedAsync(exception);
        }
    }
}