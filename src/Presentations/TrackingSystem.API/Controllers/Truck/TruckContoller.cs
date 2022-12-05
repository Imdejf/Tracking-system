using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Attributes;
using TrackingSystem.Shared.Enums.Permissions;
using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Models;
using TrackingSystem.Application.Features.Truck.Query;
using TrackingSystem.Application.Features.Truck.Command;

namespace TrackingSystem.Api.Controllers.Truck
{
    [Route("/api/Truck")]
    public class TruckContoller : BaseApiController
    {
        private const string _url = "https://widziszwszystko.eu/atlas/";

        public TruckContoller()
        {

        }

        [HttpGet]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.GetTruckById, PermissionValidationMethod.HasAll)]
        [Route("ById/{truckId}")]
        public async Task<IActionResult> GetTruckById(int truckId)
        {
            return Ok(ApiResponse.Success(200, new GetTruckById.Query(truckId)));
        }

        [HttpGet]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.GetAllTruck, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> GetAllTruck()
        {
            return Ok(ApiResponse.Success(200, new GetTruck.Query()));
        }

        [HttpGet]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.GetTruckByUserId, PermissionValidationMethod.HasAll)]
        [Route("{userId}")]
        public async Task<IActionResult> GetTruckByUserId(Guid userId)
        {
            return Ok(ApiResponse.Success(200, new GetTruckByUserId.Query(userId)));
        }

        [HttpPost]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.AddUserToTruck, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> AddUserToTruck(Guid userId, int truckId)
        {
            return Ok(ApiResponse.Success(200, new AddUserToTruck.Command(userId, truckId)));
        }

        [HttpDelete]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.RemoveUserFromTruck, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> RemoveUserFromTruck(Guid userId, int truckId)
        {
            return Ok(ApiResponse.Success(200, new RemoveUserFromTruck.Command(userId, truckId)));
        }
    }
}
