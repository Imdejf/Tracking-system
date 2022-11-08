using Microsoft.AspNetCore.Mvc;
using TrackingSystem.Application.Features.User.Comand;
using TrackingSystem.Application.Features.User.Query;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Api.Controllers.User.User
{
    [Route("api/User")]
    public class UserContoller : BaseApiController
    {
        public UserContoller()
        {

        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetById(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserById.Query(userId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpGet]
        [Route("GetUserList/{userId}")]
        public async Task<IActionResult> GetUserList(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllUser.Query(userId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUser.Command command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> RemoveUssr(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new RemoveUser.Command(userId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

    }
}
