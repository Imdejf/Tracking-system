using Microsoft.AspNetCore.Mvc;
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

    }
}
