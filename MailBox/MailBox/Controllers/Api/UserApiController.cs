using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailBox.Models.UserModels;
using MailBox.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            _userService.UpdateUserRole(userRoleUpdate);
            return Ok();
        }


        [HttpDelete]
        public IActionResult DeleteUser([FromBody] DeletedUser deletedUser)
        {
            _userService.RemoveUser(deletedUser);
            return Ok();
        }
    }
}
