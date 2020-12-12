
using System.Linq;
using MailBox.Contracts.Responses;
using MailBox.Models.UserModels;
using MailBox.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MailBox.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserApiController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult GlobalList()
        {
            return new JsonResult(_userService.GetGlobalContactList());
        }

        public IActionResult AdminViewList()
        {
            return new JsonResult(_userService.GetAdminViewList());
        }

        [HttpPut]
        public IActionResult UpdateUserRole([FromBody] UserRoleUpdate userRoleUpdate)
        {
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


        [HttpDelete]
        public IActionResult DeleteUser([FromBody] DeletedUser deletedUser)
        {
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
