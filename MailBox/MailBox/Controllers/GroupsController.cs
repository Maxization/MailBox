
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace MailBox.Controllers
{
    [Authorize]
    [Authorize(Policy = "AssignToUser")]
    public class GroupsController : Controller
    {
        public GroupsController() { }

        public IActionResult ManageGroups()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return View();
        }
    }
}
