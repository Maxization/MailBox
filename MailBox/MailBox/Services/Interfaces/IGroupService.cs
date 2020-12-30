
using System.Collections.Generic;
using MailBox.Models.GroupModels;

namespace MailBox.Services.Interfaces
{
    public interface IGroupService
    {
        public List<GroupView> GetUserGroupsList(int userID);
        public void UpdateGroupName(GroupNameUpdate groupNameUpdate);
        public void AddGroup(int ownerID, NewGroup newGroup);
        public void RemoveGroup(int groupID);
        public void AddUserToGroup(GroupMemberUpdate groupMemberUpdate);
        public void RemoveUserFromGroup(GroupMemberUpdate groupMemberUpdate);
    }
}
