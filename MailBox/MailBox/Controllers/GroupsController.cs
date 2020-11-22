
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailBox.Services.ServicesInterfaces;
using MailBox.Models.GroupModels;

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

        public ActionResult ChangeGroupName(GroupNameUpdate groupNameUpdate)
        {
            groupService.ChangeGroupName(groupNameUpdate);
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View("GroupsList");
        }

        public ActionResult NewGroup(NewGroup newGroup)
        {
            groupService.AddGroup(newGroup, 1);
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View("GroupsList");
        }

        public ActionResult DeleteGroup(int groupID)
        {
            groupService.DeleteGroup(groupID);
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View("GroupsList");
        }

        public ActionResult AddUserToGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.AddUserToGroup(groupMemberUpdate);
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View("GroupsList");
        }

        public ActionResult DeleteUserFromGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.DeleteUserFromGroup(groupMemberUpdate);
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View("GroupsList");
        }
    }
}
