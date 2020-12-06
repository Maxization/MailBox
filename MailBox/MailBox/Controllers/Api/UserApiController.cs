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
            _userService.SetUserRole(userRoleUpdate);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUserEnableStatus([FromBody] UserEnableUpdate userEnableUpdate)
        {
            _userService.SetUserEnableStatus(userEnableUpdate);
            return Ok();
        }
    }
}
