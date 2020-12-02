using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MailBox.Contracts.Responses;
using MailBox.Models.GroupModels;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailBox.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class GroupsApiController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupsApiController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public IActionResult GetUserGroupsListAsJson()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var userGroups = groupService.GetUserGroupsList(userID);

            return new JsonResult(userGroups);
        }

        [HttpPut]
        public IActionResult ChangeGroupName([FromBody] GroupNameUpdate groupNameUpdate)
        {
            groupService.ChangeGroupName(groupNameUpdate);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddGroup([FromBody] NewGroup newGroup)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            groupService.AddGroup(newGroup, userID);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteGroup([FromBody] int groupID)
        {
            groupService.DeleteGroup(groupID);

            return Ok();
        }

        [HttpPost]
        public IActionResult AddUserToGroup([FromBody] GroupMemberUpdate groupMemberUpdate)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                groupService.AddUserToGroup(groupMemberUpdate);
            }
            catch (Exception ex)
            {
                errorResponse.Errors.Add(new ErrorModel { FieldName = ex.Message, Message = ex.InnerException.Message });
                Response.StatusCode = 400;
            }
            return new JsonResult(errorResponse);
        }

        [HttpDelete]
        public IActionResult DeleteUserFromGroup([FromBody] GroupMemberUpdate groupMemberUpdate)
        {
            groupService.DeleteUserFromGroup(groupMemberUpdate);
            return Ok();
        }
    }
}
