using TrackingSystem.Application.Features.User.Dto;
using TrackingSystem.Domain.Entities.Truck;

namespace TrackingSystem.Application.Features.Truck.Query.Dto
{
    public class TruckDto
    {
        public Guid Id { get; set; }
        public int TruckId { get; set; }
        public string RegisterNumber { get; set; }
        public TruckDetailsDto TruckDetails { get; set; }
        public UserDto User { get; set; }

        public static TruckDto CreateFromEntity(TruckEntity entity)
        {
            return new TruckDto
            {
                Id = entity.Id,
                TruckId = entity.TruckId,
                RegisterNumber = entity.RegisterNumber,
                User = new UserDto
                {
                    Id = entity.User.Id,
                    FirstName = entity.User.FirstName,
                    LastName = entity.User.LastName,
                    FilePath = entity.User.FilePath,
                    Name = entity.User.Name,
                    Language = entity.User.Language,
                },
                TruckDetails = new TruckDetailsDto
                {
                    LastLocalizationDate = entity.TruckDetails.LastLocalizationDate,
                    Heading = entity.TruckDetails.Heading,
                    IgnitionState = entity.TruckDetails.IgnitionState,
                    LastLatitude = entity.TruckDetails.LastLatitude,
                    LastLongitude = entity.TruckDetails.LastLongitude,
                    Speed = entity.TruckDetails.Speed,
                    TruckId = entity.TruckDetails.TruckId,
                },
            };
        }
    }
}
