
using System;
using System.Linq;
using System.Security.Claims;
using MailBox.Contracts.Responses;
using MailBox.Models.GroupModels;
using MailBox.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Gets Groups that belongs to user
        /// </summary>
        /// <returns>Groups in JSON format</returns>
        [HttpGet]
        public IActionResult GetUserGroupsListAsJson()
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var userGroups = groupService.GetUserGroupsList(userID);

            return new JsonResult(userGroups);
        }

        /// <summary>
        /// Change group name
        /// </summary>
        /// <param name="groupNameUpdate"></param>
        /// <returns>OK</returns>
        [HttpPut]
        public IActionResult ChangeGroupName([FromBody] GroupNameUpdate groupNameUpdate)
        {
            groupService.UpdateGroupName(groupNameUpdate);
            return Ok();
        }

        /// <summary>
        /// Add user group 
        /// </summary>
        /// <param name="newGroup"></param>
        /// <returns>Ok</returns>
        [HttpPost]
        public IActionResult AddGroup([FromBody] NewGroup newGroup)
        {
            int userID = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            groupService.AddGroup(userID, newGroup);
            return Ok();
        }

        /// <summary>
        /// Remove group of given id
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns>OK</returns>
        [HttpDelete("{groupID}")]
        public IActionResult DeleteGroup(int groupID)
        {
            groupService.RemoveGroup(groupID);
            return Ok();
        }

        /// <summary>
        /// Add new user to group
        /// </summary>
        /// <param name="groupMemberUpdate"></param>
        /// <returns>Error list in JSON if any</returns>
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

        /// <summary>
        /// Delete user from group
        /// </summary>
        /// <param name="groupMemberUpdate"></param>
        /// <returns>Ok</returns>
        [HttpDelete]
        public IActionResult DeleteUserFromGroup([FromBody] GroupMemberUpdate groupMemberUpdate)
        {
            groupService.RemoveUserFromGroup(groupMemberUpdate);
            return Ok();
        }
    }
}
