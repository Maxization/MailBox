
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBox.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;

namespace MailBox.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
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
