using TrackingSystem.Domain.Entities.Abstract;
using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Domain.Entities.Truck
{
    public sealed class TruckEntity : Entity
    {
        public int TruckId { get; set; }
        public string RegisterNumber { get; set; }
        public Guid? UserId { get; set; }
        public UserEntity User { get; set; }
        public TruckDetailsEntity TruckDetails { get; set; }
        public ICollection<UserTruckEntity> UserTrucks { get; set; }
    }
}
