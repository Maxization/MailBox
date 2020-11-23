
using Microsoft.AspNetCore.Mvc;
using MailBox.Services.Interfaces;
using MailBox.Models.GroupModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using MailBox.Contracts.Responses;

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

        public void ChangeGroupName(GroupNameUpdate groupNameUpdate)
        {
            groupService.ChangeGroupName(groupNameUpdate);
        }

        public void AddGroup(NewGroup newGroup)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            groupService.AddGroup(newGroup, userID);
        }

        public void DeleteGroup(int groupID)
        {
            groupService.DeleteGroup(groupID);
        }

        public IActionResult AddUserToGroup(GroupMemberUpdate groupMemberUpdate)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                groupService.AddUserToGroup(groupMemberUpdate);
            }
            catch (Exception e)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = "GroupMemberAddress", Message = e.Message });
                Response.StatusCode = 400;
            }
            return Json(errorResponse);
        }

        public void DeleteUserFromGroup(GroupMemberUpdate groupMemberUpdate)
        {
            groupService.DeleteUserFromGroup(groupMemberUpdate);
        }
    }
}
