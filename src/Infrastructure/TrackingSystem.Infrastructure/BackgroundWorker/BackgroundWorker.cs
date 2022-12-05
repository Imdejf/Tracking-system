using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using TrackingSystem.Application.Common.Exceptions;
using TrackingSystem.Application.Common.Interfaces.DataAccess;
using TrackingSystem.Domain.Entities.Truck;
using TrackingSystem.Infrastructure.BackgroundWorker.Model;

namespace TrackingSystem.Infrastructure.BackgroundWorker
{
    internal sealed class BackgroundWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private const string getAllTruck = "https://widziszwszystko.eu/atlas/trans-sped/transsped/devices?password=hostessA6";
        private const string getTruckPosition = "https://widziszwszystko.eu/atlas/trans-sped/transsped/positions?password=hostessA6";

        public BackgroundWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(60, stoppingToken);

                    HttpClient client = new HttpClient();

                    var truckList = await client.GetAsync(getAllTruck);
                    var truckPositionList = await client.GetAsync(getTruckPosition);

                    if (truckPositionList == null) continue;

                    if(truckList != null)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                            var trucks = await unitOfWork.Trucks.GetAllAsync(stoppingToken);
                            if(trucks is null || trucks.Count == 0)
                            {
                                var truckReading = await truckList.Content.ReadAsStringAsync();
                                var list = JsonConvert.DeserializeObject<Truck>(truckReading);
                                
                                foreach(var truck in list.deviceList)
                                {
                                    var newTruck = new TruckEntity
                                    {
                                        Id = Guid.NewGuid(),
                                        RegisterNumber = truck.deviceName,
                                        TruckId = Convert.ToInt32(truck.deviceId),
                                        TruckDetails = new TruckDetailsEntity
                                        {
                                            CreatedDate = System.DateTime.UtcNow,
                                            Speed = 0,
                                            IgnitionState = false,
                                            CreatedBy = "",
                                            Heading = 1,
                                            LastLocalizationDate = System.DateTime.UtcNow,
                                            LastModifiedDate = System.DateTime.UtcNow,
                                            LastLatitude = 1,
                                            LastLongitude = 1,
                                            LastModifiedBy = "",
                                        }
                                    };

                                    await unitOfWork.Trucks.AddAsync(newTruck, stoppingToken);
                                }
                            }

                            await unitOfWork.SaveChangesAsync(stoppingToken);
                        }
                    }

                    if (truckPositionList != null)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                            var truckReading = await truckPositionList.Content.ReadAsStringAsync();
                            var positionDetails = JsonConvert.DeserializeObject<TruckDetails>(truckReading);

                            foreach(var truck in positionDetails.positionList)
                            {
                                var currentTruck = await unitOfWork.TruckDetails.GetById(Convert.ToInt32(truck.deviceId), stoppingToken);
                                if(truck.ignitionState == "OFF")
                                {
                                    currentTruck.IgnitionState = false;
                                }
                                else
                                {
                                    currentTruck.IgnitionState = true;
                                }
                                currentTruck.LastLatitude = Convert.ToDecimal(truck.coordinate.latitude);
                                currentTruck.LastLongitude = Convert.ToDecimal(truck.coordinate.longitude);
                                currentTruck.LastLocalizationDate = System.DateTime.Parse($"{truck.dateTime.year}-{truck.dateTime.month.ToString("D2")}-{truck.dateTime.day.ToString("D2")}T{truck.dateTime.hour.ToString("D2")}:{truck.dateTime.minute.ToString("D2")}:{truck.dateTime.seconds.ToString("D2")}.000Z").ToUniversalTime();
                                currentTruck.Heading = truck.heading;
                                currentTruck.Speed = truck.speed;

                                unitOfWork.TruckDetails.Update(currentTruck);
                            }

                            await unitOfWork.SaveChangesAsync(stoppingToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new BackgroundWorkerException($"Background worker an error occurred when remove muted conversation. Exception: {ex}");
                }
            }
        }
    }
}