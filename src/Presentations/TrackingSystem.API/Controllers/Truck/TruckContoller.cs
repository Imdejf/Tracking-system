using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Api.Controllers.Truck
{
    [Route("/api/Permissions")]
    public class TruckContoller : BaseApiController
    {
        private const string _url = "https://widziszwszystko.eu/atlas/";

        public TruckContoller()
        {

        }

        [HttpGet]
        [Authorize]
        [Route("Devices")]
        public async Task<IActionResult> GetAllTruck()
        {
            var client = new HttpClient();
            var result = await client.GetAsync(_url+"/trans-sped/transsped/devices?password=hostessA6");

            return Ok(ApiResponse.Success(200, result.Content.ReadAsStringAsync()));
        }

        [HttpGet]
        [Authorize]
        [Route("Positions")]
        public async Task<IActionResult> GetAllPositionTruck()
        {
            var client = new HttpClient();
            var result = await client.GetAsync(_url+"/trans-sped/transsped/positions?password=hostessA6");

            return Ok(ApiResponse.Success(200, result.Content.ReadAsStringAsync()));
        }
    }
}
