using TrackingSystem.Domain.Entities.Identity;

namespace TrackingSystem.Domain.Entities.Truck
{
    public sealed class UserTruckEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public int TruckId { get; set; }
        public TruckEntity Truck { get; set; }

    }
}
