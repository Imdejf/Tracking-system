using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingSystem.Application.Features.Event.Command;
using TrackingSystem.Application.Features.Event.Query;
using TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Query;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Api.Controllers.Event
{
    [Route("/api/Event")]

    public class EventController : BaseApiController
    {
        [HttpGet]
        //[Authorize]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> GetAllEvents(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllEvent.Query(), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpGet]
        //[Authorize]
        [AllowAnonymous]
        [Route("{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetEventById.Query(eventId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpPost]
        //[Authorize]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> CreateEvent(CreateEvent.Command command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        [HttpPut]
        //[Authorize]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> UpdateEvent(UpdateEvent.Command command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }


        [HttpDelete]
        //[Authorize]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> DeleteEvent(RemoveEvent.Command command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }
    }
}
