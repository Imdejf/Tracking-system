using TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Command;
using TrackingSystem.Application.Features.CommonFeatures.AuthFeatures.Query;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Exceptions;
using TrackingSystem.Shared.Models;
using TrackingSystem.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrackingSystem.Api.Controllers
{
    /// <summary>
    /// Controller responsible for User identity management
    /// </summary>
    [Route("/api/Identity")]

    public class IdentityController : BaseApiController
    {
        private readonly ICurrentUserService _CurrentUserService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="currentUserService"></param>
        public IdentityController(ICurrentUserService currentUserService)
        {
            _CurrentUserService = currentUserService;
        }

        /// <summary>
        /// Refreshes current user JWT
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>JwtResponse</returns>
        [HttpGet]
        [Authorize]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new RefreshToken.Query(), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        /// <summary>
        /// Resends EmailConfirmation mail to passed reciver email addres
        /// </summary>
        /// <param name="reciverEmail">Email</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ResendEmailConfirmation/{reciverEmail}")]
        public async Task<IActionResult> ResendEmailConfirmation(string reciverEmail, CancellationToken cancellationToken)
        {
            _ = await Mediator.Send(new ResendEmailConfirmationEmail.Query(reciverEmail), cancellationToken);
            return Ok(ApiResponse.Success(200, null));
        }

        /// <summary>
        /// Sends PasswordReset mail to passed reciver email addres
        /// </summary>
        /// <param name="reciverEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("SendPasswordResetEmail/{reciverEmail}")]
        public async Task<IActionResult> SendPasswordResetEmail(string reciverEmail, CancellationToken cancellationToken)
        {
            _ = await Mediator.Send(new SendPasswordResetEmail.Query(reciverEmail), cancellationToken);
            return Ok(ApiResponse.Success(200, null));
        }

        /// <summary>
        /// Changes current user password
        /// </summary>
        /// <param name="command">Password change request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword.Command command, CancellationToken cancellationToken)
        {
            if (command.UserId != _CurrentUserService.CurrentUser.Id)
            {
                throw new InvalidRequestException("Passed UserId is not equal to UserId binded from JWT");
            }

            _ = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, null));
        }

        /// <summary>
        /// Confirms users email
        /// </summary>
        /// <param name="command">Email confirmation request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmail.Command command, CancellationToken cancellationToken)
        {
            _ = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, null));
        }

        /// <summary>
        /// Removes users account
        /// </summary>
        /// <param name="command">Account removal request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RemoveAccount")]
        public async Task<IActionResult> RemoveAccount(RemoveAccount.Command command, CancellationToken cancellationToken)
        {
            if (command.UserId != _CurrentUserService.CurrentUser.Id)
            {
                throw new InvalidRequestException("Passed UserId is not equal to UserId binded from JWT");
            }

            _ = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, null));
        }

        /// <summary>
        /// Try to login user using passed credentials
        /// </summary>
        /// <param name="query">Login Credentials</param>
        /// <param name="cancellationToken"></param>
        /// <returns>JwtResponse</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(Login.Query query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="command">Register request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(Register.Command command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(201, result));
        }

        /// <summary>
        /// Resets users password
        /// </summary>
        /// <param name="command">Password reset request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword.Command command, CancellationToken cancellationToken)
        {
            _ = await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, null));
        }
    }
}