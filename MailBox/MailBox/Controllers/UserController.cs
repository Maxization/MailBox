
using Microsoft.AspNetCore.Mvc;
using MailBox.Services;
using Microsoft.AspNetCore.Authorization;

namespace MailBox.Controllers
{
    [Authorize(Policy = "AssignToUser")]
    public class UserController : Controller
    {
        public UserController() { }

        public IActionResult Index()
        {
            return View("GlobalList");
        }

        [Authorize(Policy = "AssignToAdmin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
