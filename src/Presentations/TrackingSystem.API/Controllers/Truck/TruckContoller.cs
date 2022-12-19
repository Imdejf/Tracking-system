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
            var result = await Mediator.Send(new GetTruckById.Query(truckId));
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpGet]
        [Authorize]
        //[VerifyPermissions(TruckServicePermissions.GetTruckById, PermissionValidationMethod.HasAll)]
        [Route("ActiveTruck")]
        public async Task<IActionResult> GetAllActiveTruck()
        {
            var result = await Mediator.Send(new GetActiveTruck.Query());
            return Ok(ApiResponse.Success(200, result));
        }


        [HttpGet]
        //[Authorize]
        //[VerifyPermissions(TruckServicePermissions.GetTruckByUserId, PermissionValidationMethod.HasAll)]
        [Route("{userId}")]
        public async Task<IActionResult> GetTruckByUserId(Guid userId)
        {
            var result = await Mediator.Send(new GetTruckByUserId.Query(userId));
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpPost]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.AddUserToTruck, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> AddUserToTruck(Guid userId, int truckId)
        {
            var result = await Mediator.Send(new AddUserToTruck.Command(userId, truckId));
            return Ok(ApiResponse.Success(200, result));
        }


        [HttpPut]
        //[Authorize]
        [Route("/trucker")]
        public async Task<IActionResult> AddTrucker(Guid userId, int truckId)
        {
            var result = await Mediator.Send(new AddTrucker.Command(userId, truckId));
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpDelete]
        [Authorize]
        [VerifyPermissions(TruckServicePermissions.RemoveUserFromTruck, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> RemoveUserFromTruck(Guid userId, int truckId)
        {
            var result = await Mediator.Send(new RemoveUserFromTruck.Command(userId, truckId));
            return Ok(ApiResponse.Success(200, result));
        }
    }
}
