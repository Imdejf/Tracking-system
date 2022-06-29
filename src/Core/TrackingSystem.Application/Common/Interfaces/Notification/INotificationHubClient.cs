namespace TrackingSystem.Application.Common.Interfaces.Notification
{
    public interface INotificationHubClient
    {
        Task SendAsync(Guid userId, string description, CancellationToken cancellationToken);
    }
}
