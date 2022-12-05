using TrackingSystem.Domain.Entities.Abstract;

namespace TrackingSystem.Domain.Entities.Truck
{
    public sealed class TruckEntity : Entity
    {
        public int TruckId { get; set; }
        public string RegisterNumber { get; set; }
        public TruckDetailsEntity TruckDetails { get; set; }
        public ICollection<UserTruckEntity> UserTrucks { get; set; }
    }
}
