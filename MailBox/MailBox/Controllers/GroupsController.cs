
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

        public ActionResult ManageGroups()
        {
            ViewData["groups"] = groupService.GetUserGroupsList(1);
            return View();
        }

        public ActionResult ChangeGroupName(GroupNameUpdate groupNameUpdate)
        {
            groupService.ChangeGroupName(groupNameUpdate);
            return RedirectToAction("ManageGroups");
        }

        public ActionResult AddGroup(NewGroup newGroup)
        {
            groupService.AddGroup(newGroup, 1);
            return RedirectToAction("ManageGroups");
        }

        public ActionResult DeleteGroup(int groupID)
        {
            groupService.DeleteGroup(groupID);
            return RedirectToAction("ManageGroups");
        }

        public ActionResult AddUserToGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.AddUserToGroup(groupMemberUpdate);
            return RedirectToAction("ManageGroups");
        }

        public ActionResult DeleteUserFromGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.DeleteUserFromGroup(groupMemberUpdate);
            return RedirectToAction("ManageGroups");
        }
    }
}
