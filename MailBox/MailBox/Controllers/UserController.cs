
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBox.Services.ServicesInterfaces;

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

        public IActionResult Index()
        {
            return View("GlobalList");
        }

        public IActionResult GlobalList()
        {
            return Json(_userService.GetGlobalContactList());
        }
    }
}
