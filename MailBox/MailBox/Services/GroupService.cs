
using System.Linq;
using System.Collections.Generic;
using MailBox.Models.GroupModels;
using MailBox.Models.UserModels;
using MailBox.Database;
using MailBox.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MailBox.Services
{
    public class GroupService : IGroupService
    {
        private readonly MailBoxDBContext context;
        private IConfiguration configuration;

        public GroupService(MailBoxDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public List<GroupView> GetUserGroupsList(int userID)
        {
            var groups = new List<GroupView>();
            var dbGroups = context.Groups.Where(g => g.Owner.ID == userID).AsQueryable();
            foreach (var dbG in dbGroups)
            {
                var dbGroupUsers = context.GroupUsers.Where(gu => gu.GroupID == dbG.ID).AsQueryable();
                groups.Add(new GroupView { GroupID = dbG.ID, Name = dbG.GroupName, GroupMembers = GetGroupMembers(dbGroupUsers.ToList()) });
            }
            return groups;
        }

        public List<UserGlobalView> GetGroupMembers(List<GroupUser> dbGroupUsers)
        {
            var groupMembers = new List<UserGlobalView>();
            foreach (var dbGU in dbGroupUsers)
            {
                var gMember = context.Users.Find(dbGU.UserID);
                groupMembers.Add(new UserGlobalView { Name = gMember.FirstName, Surname = gMember.LastName, Address = gMember.Email });
            }
            return groupMembers;
        }

        public void ChangeGroupName(GroupNameUpdate gnu)
        {
            Group group = context.Groups.Find(gnu.GroupID);
            group.GroupName = gnu.Name;
            context.SaveChanges();
        }

        public void AddGroup(NewGroup ng, int ownerID)
        {
            User owner = context.Users.Find(ownerID);
            Group newGroup = new Group
            {
                Owner = owner,
                GroupName = ng.Name
            };
            context.Groups.Add(newGroup);
            context.SaveChanges();
        }

        public void DeleteGroup(int groupID)
        {
            Group group = context.Groups.Find(groupID);
            context.Groups.Remove(group);
            context.SaveChanges();
        }

        public void AddUserToGroup(GroupMemberUpdate gmu)
        {
            User user = context.Users.Where(u => u.Email == gmu.GroupMemberAddress).AsQueryable().First();
            GroupUser groupUser = new GroupUser
            {
                UserID = user.ID,
                GroupID = gmu.GroupID
            };
            context.GroupUsers.Add(groupUser);
            context.SaveChanges();
        }

        public void DeleteUserFromGroup(GroupMemberUpdate gmu)
        {
            User user = context.Users.Where(u => u.Email == gmu.GroupMemberAddress).AsQueryable().First();
            GroupUser groupUser = context.GroupUsers.Find(gmu.GroupID, user.ID);
            context.GroupUsers.Remove(groupUser);
            context.SaveChanges();
        }
    }
}
