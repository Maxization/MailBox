
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using MailBox.Models.GroupModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace MailBox.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public IActionResult ManageGroups()
        {
            ViewData["Groups"] = GetUserGroups();
            return View();
        }

        public IActionResult GetUserGroupsListAsJson()
        {
            return Json(GetUserGroups());
        }

        private List<GroupView> GetUserGroups()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return groupService.GetUserGroupsList(userID);
        }

        public IActionResult ChangeGroupName(GroupNameUpdate groupNameUpdate)
        {
            groupService.ChangeGroupName(groupNameUpdate);
            return RedirectToAction("ManageGroups");
        }

        public IActionResult AddGroup(NewGroup newGroup)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            groupService.AddGroup(newGroup, userID);
            return RedirectToAction("ManageGroups");
        }

        public IActionResult DeleteGroup(int groupID)
        {
            groupService.DeleteGroup(groupID);
            return RedirectToAction("ManageGroups");
        }

        public IActionResult AddUserToGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.AddUserToGroup(groupMemberUpdate);
            return RedirectToAction("ManageGroups");
        }

        public IActionResult DeleteUserFromGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.DeleteUserFromGroup(groupMemberUpdate);
            return RedirectToAction("ManageGroups");
        }
    }
}
