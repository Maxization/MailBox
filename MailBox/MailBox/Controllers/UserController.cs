
using Microsoft.AspNetCore.Mvc;
using MailBox.Services;
using Microsoft.AspNetCore.Authorization;

namespace MailBox.Controllers
{
    [Authorize(Policy = "AssignToAdmin")]
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

        public IActionResult AdminPanel()
        {
            return View();
        }

    }
}
