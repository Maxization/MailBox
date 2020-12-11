
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
    [Authorize(Policy = "AssignToUser")]
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public IActionResult ManageGroups()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            ViewData["Groups"] = groupService.GetUserGroupsList(userID);
            return View();
        }

    }
}
