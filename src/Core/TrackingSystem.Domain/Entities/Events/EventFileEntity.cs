using TrackingSystem.Domain.Entities.Abstract;

namespace TrackingSystem.Domain.Entities.Events
{
    public class EventFileEntity : Entity
    {
        public string FilePath { get; set; }
        public Guid EventId { get; set; }
        public EventEntity Event { get; set; }
    }
}
