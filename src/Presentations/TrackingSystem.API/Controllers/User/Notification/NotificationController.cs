using TrackingSystem.Application.Features.CommonFeatures.Notification.Command;
using TrackingSystem.Application.Features.CommonFeatures.Notification.Query;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrackingSystem.API.Controllers.User.Notification
{
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : BaseApiController
    {
        private readonly ICurrentUserService _currentUserService;
        public NotificationController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetLatestNotification(GetLatestNotificationPageByUserId.Query query, CancellationToken cancellationToken = default)
        {
            if (_currentUserService.CurrentUser.Id != query.UserId)
            {
                throw new InvalidRequestException("Send invalid user id");
            }

            return Ok(ApiResponse.Success(200, await Mediator.Send(query, cancellationToken)));
        }

        [HttpPost]
        [Route("Subscribe")]
        public async Task<IActionResult> SubscribeNotification(SubscribeToNotification.Command command, CancellationToken cancellationToken)
        {
            if (_currentUserService.CurrentUser.Id != command.UserId)
            {
                throw new InvalidRequestException("Send invalid user id");
            }

            return Ok(ApiResponse.Success(200, await Mediator.Send(command, cancellationToken)));

        }

        [HttpPost]
        [Route("Unsubscribe")]
        public async Task<IActionResult> UnsubscribeNotification(UnsubscribeFromNotification.Command command, CancellationToken cancellationToken)
        {
            if (_currentUserService.CurrentUser.Id != command.UserId)
            {
                throw new InvalidRequestException("Send invalid user id");
            }

            return Ok(ApiResponse.Success(200, await Mediator.Send(command, cancellationToken)));
        }

        [HttpPut]
        [Route("See")]
        public async Task<IActionResult> SeeNotification(SetSeeNotification.Command command, CancellationToken cancellationToken)
        {
            if (_currentUserService.CurrentUser.Id != command.UserId)
            {
                throw new InvalidRequestException("Send invalid user id");
            }

            return Ok(ApiResponse.Success(200, await Mediator.Send(command, cancellationToken)));
        }

        [HttpPut]
        [Route("Complete")]
        public async Task<IActionResult> CompleteNotification(CompleteNotification.Command command, CancellationToken cancellationToken)
        {
            if (_currentUserService.CurrentUser.Id != command.UserId)
            {
                throw new InvalidRequestException("Send invalid user id");
            }

            return Ok(ApiResponse.Success(200, await Mediator.Send(command, cancellationToken)));

        }
    }
}
