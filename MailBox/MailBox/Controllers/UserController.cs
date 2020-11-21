using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBox.Models;
using MailBox.Services.ServicesInterfaces;
using MailBox.Models.UserModels;
using MailBox.Validators.UserValidators;

namespace MailBox.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult GlobalList()
        {
            return Json(_userService.GetGlobalContactList());
        }
    }
}
