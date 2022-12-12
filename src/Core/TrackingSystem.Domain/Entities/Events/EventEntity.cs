using Microsoft.Extensions.Configuration.UserSecrets;
using TrackingSystem.Domain.Entities.Abstract;
using TrackingSystem.Domain.Entities.Identity;
using TrackingSystem.Domain.Enums;

namespace TrackingSystem.Domain.Entities.Events
{
    public class EventEntity : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventType EventType { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<EventFileEntity> EventFiles { get; set; }
    }
}
