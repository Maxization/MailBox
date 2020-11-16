
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBox.Services.ServicesInterfaces;

namespace MailBox.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ILogger<GroupsController> logger;
        private readonly IGroupService groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            this.logger = logger;
            this.groupService = groupService;
        }

        public ActionResult GroupsList()
        {
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View();
        }
    }
}
