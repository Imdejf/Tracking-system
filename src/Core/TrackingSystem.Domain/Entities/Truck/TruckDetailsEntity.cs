using TrackingSystem.Domain.Entities.Abstract;

namespace TrackingSystem.Domain.Entities.Truck
{
    public sealed class TruckDetailsEntity : Entity
    {
        public int TruckId { get; set; }
        public decimal LastLatitude { get; set; }
        public decimal LastLongitude { get; set; }
        public DateTime LastLocalizationDate { get; set; }
        public bool IgnitionState { get; set; }
        public int Speed { get; set; }
        public int Heading { get; set; }
        public TruckEntity Truck { get; set; }
    }
}
