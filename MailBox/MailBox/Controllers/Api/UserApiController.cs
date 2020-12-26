
using System.Collections.Generic;
using System.Linq;
using MailBox.Contracts.Responses;
using MailBox.Models.UserModels;
using MailBox.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MailBox.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "AssignToUser")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        readonly TelemetryClient _telemetryClient;
        public UserApiController(IUserService userService, TelemetryClient telemetryClient)
        {
            _userService = userService;
            _telemetryClient = telemetryClient;
        }

        /// <summary>
        /// Gets all user emails
        /// </summary>
        /// <returns>List of emails in JSON</returns>
        [HttpGet]
        public IActionResult GlobalList()
        {
            _telemetryClient.TrackEvent("Get");
            return new JsonResult(_userService.GetGlobalContactList());
        }

        /// <summary>
        /// Gets informations about all users
        /// </summary>
        /// <returns>List of user data in JSON</returns>
        [Authorize(Policy = "AssignToAdmin")]
        [HttpGet]
        public IActionResult AdminViewList()
        {
            _telemetryClient.TrackEvent("Get");
            return new JsonResult(_userService.GetAdminViewList());
        }

        /// <summary>
        /// Update role of selected user
        /// </summary>
        /// <param name="userRoleUpdate"></param>
        /// <returns>Ok</returns>
        [Authorize(Policy = "AssignToAdmin")]
        [HttpPut]
        public IActionResult UpdateUserRole([FromBody] UserRoleUpdate userRoleUpdate)
        {
            _telemetryClient.TrackEvent("Put");
            ErrorResponse errorResponse = new ErrorResponse();
            string userEmail = User.Claims.Where(x => x.Type == "emails").First().Value;
            if (userEmail == userRoleUpdate.Address)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = "User", Message = "You can't change your role" });
                Response.StatusCode = 400;
                return new JsonResult(errorResponse);
            }
            _userService.UpdateUserRole(userRoleUpdate);
            return Ok();
        }

        /// <summary>
        /// Delete selected user
        /// </summary>
        /// <param name="deletedUser"></param>
        /// <returns>Ok</returns>
        [Authorize(Policy = "AssignToAdmin")]
        [HttpDelete]
        public IActionResult DeleteUser([FromBody] DeletedUser deletedUser)
        {
            _telemetryClient.TrackEvent("Delete",
                new Dictionary<string, string>() { { "Address", deletedUser.Address } });
            ErrorResponse errorResponse = new ErrorResponse();
            string userEmail = User.Claims.Where(x => x.Type == "emails").First().Value;

            if (userEmail == deletedUser.Address)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = "User", Message = "You can't remove yourself" });
                Response.StatusCode = 400;
                return new JsonResult(errorResponse);
            }
            _userService.RemoveUser(deletedUser);
            return Ok();
        }
    }
}
