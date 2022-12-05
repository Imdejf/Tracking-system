using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Infrastructure.BackgroundWorker.Model
{
    public class Truck
    {
        public List<DeviceList> deviceList { get; set; }
    }
    public class DeviceList
    {
        public string deviceId { get; set; }
        public string deviceName { get; set; }
    }
}
