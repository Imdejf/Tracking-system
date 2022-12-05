using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Infrastructure.BackgroundWorker.Model
{
    public class Coordinate
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class DateTime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int seconds { get; set; }
        public string timezone { get; set; }
    }

    public class PositionList
    {
        public string deviceId { get; set; }
        public Coordinate coordinate { get; set; }
        public DateTime dateTime { get; set; }
        public int heading { get; set; }
        public int speed { get; set; }
        public string ignitionState { get; set; }
    }

    public class TruckDetails
    {
        public List<PositionList> positionList { get; set; }
    }

}
