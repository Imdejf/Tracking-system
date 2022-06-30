using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Command;
using TrackingSystem.Application.Features.ManagemenetFeatures.Permission.Query;
using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Attributes;
using TrackingSystem.Shared.Enums;
using TrackingSystem.Shared.Enums.Permissions;
using TrackingSystem.Shared.Models;

namespace TrackingSystem.Api.Controllers.ManagementController
{
    /// <summary>
    /// Controller responsible for routing Permissions domain requests
    /// </summary>
    [Route("/api/Permissions")]
    public class PermissionsController : BaseApiController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">MediatR instance</param>
        public PermissionsController()
        {
        }

        /// <summary>
        /// Gets all existings profiles from PermissionStorage
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet]
        //[Authorize]
        [AllowAnonymous]
        [Route("/api/Profiles")]
        public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllProfile.Query(), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        /// <summary>
        /// Gets all existing permissions from PermissionStorage
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet]
        [Authorize]
        [VerifyPermissions(AuthServicePermissions.ManagePermissions | AuthServicePermissions.GrantPermission, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> GetAllPermissions(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllPermission.Query(), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        /// <summary>
        /// Gets all existing permissions from PermissionsStorage with additional marking of ones which passed user owns
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet]
        [Authorize]
        [VerifyPermissions(AuthServicePermissions.ManagePermissions, PermissionValidationMethod.HasAll)]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserPermissions(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserPermission.Query(userId), cancellationToken);
            return Ok(ApiResponse.Success(200, result));
        }

        /// <summary>
        /// Updates passed users permissions by revoking and granting passed collection.
        /// </summary>
        /// <param name="updatePermissionsCommand">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpPut]
        [Authorize]
        [VerifyPermissions(AuthServicePermissions.ManagePermissions, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> UpdatePermissions(UpdatePermissions.Command command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, ""));
        }

        /// <summary>
        /// Grants single permission to passed user 
        /// </summary>
        /// <param name="grantPermissionCommand">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpPost]
        [Authorize]
        [VerifyPermissions(AuthServicePermissions.ManagePermissions | AuthServicePermissions.GrantPermission, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> GrantPermission(GrantPermission.Command grantPermissionCommand, CancellationToken cancellationToken)
        {
            await Mediator.Send(grantPermissionCommand, cancellationToken);
            return Ok(ApiResponse.Success(201, ""));
        }

        /// <summary>
        /// Revokes single permission from passed user
        /// </summary>
        /// <param name="revokePermissionCommand">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete]
        [AllowAnonymous]
        [Authorize]
        [VerifyPermissions(AuthServicePermissions.ManagePermissions | AuthServicePermissions.RevokePermission, PermissionValidationMethod.HasAll)]
        [Route("")]
        public async Task<IActionResult> RevokePermission(RevokePermission.Command command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, ""));
        }

        /// <summary>
        /// Assigns passed user to PermissionProfile granting him collection of predefined permission
        /// </summary>
        /// <param name="assignToProfileCommand">Command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpPost]
        [Authorize]
        [VerifyPermissions(AuthServicePermissions.ManagePermissions | AuthServicePermissions.GrantPermission | AuthServicePermissions.RevokePermission, PermissionValidationMethod.HasAll)]
        [Route("AssignToProfile")]
        public async Task<IActionResult> AssignToProfile(AssignToProfile.Command command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(ApiResponse.Success(200, ""));
        }

    }
}
