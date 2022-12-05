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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContoller(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetById(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserById.Query(userId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpGet]
        [Route("GetIp")]
        public async Task<IActionResult> GetIp(CancellationToken cancellationToken)
        {
            return Ok(ApiResponse.Success(200, _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()));
        }

        [HttpGet]
        [Route("GetUserList/{userId}")]
        public async Task<IActionResult> GetUserList(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllUser.Query(userId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpPut]
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
