using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
