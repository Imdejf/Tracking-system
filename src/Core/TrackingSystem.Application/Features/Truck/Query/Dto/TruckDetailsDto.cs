using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Features.Truck.Query.Dto
{
    public sealed class TruckDetailsDto
    {
        public int TruckId { get; set; }
        public decimal LastLatitude { get; set; }
        public decimal LastLongitude { get; set; }
        public DateTime LastLocalizationDate { get; set; }
        public bool IgnitionState { get; set; }
        public int Speed { get; set; }
        public int Heading { get; set; }
    }
}
