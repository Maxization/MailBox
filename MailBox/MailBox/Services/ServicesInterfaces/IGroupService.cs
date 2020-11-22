
using System.Collections.Generic;
using MailBox.Models.GroupModels;

namespace MailBox.Services.ServicesInterfaces
{
    public interface IGroupService
    {
        public List<GroupView> GetUserGroupsList(int userID);
        public void ChangeGroupName(GroupNameUpdate gnu);
        public void AddGroup(NewGroup ng, int ownerID);
        public void DeleteGroup(int groupID);
        public void AddUserToGroup(GroupMemberUpdate gmu);
        public void DeleteUserFromGroup(GroupMemberUpdate gmu);
    }
}
