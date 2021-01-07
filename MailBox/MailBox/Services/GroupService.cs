
using System.Linq;
using System.Collections.Generic;
using MailBox.Models.GroupModels;
using MailBox.Models.UserModels;
using MailBox.Database;
using MailBox.Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;

namespace MailBox.Services
{
    public class GroupService : IGroupService
    {
        private readonly MailBoxDBContext _context;
        public GroupService(MailBoxDBContext context)
        {
            _context = context;
        }

        public List<GroupView> GetUserGroupsList(int userID)
        {
            var groups = new List<GroupView>();
            var dbGroups = _context.Groups.Include("GroupUsers.User").Where(g => g.Owner.ID == userID).ToList();
            foreach (var dbG in dbGroups)
            {
                groups.Add(new GroupView { GroupID = dbG.ID, Name = dbG.GroupName, GroupMembers = GetGroupMembers(dbG.GroupUsers.ToList()) });
            }
            return groups;
        }

        public List<UserGlobalView> GetGroupMembers(List<GroupUser> dbGroupUsers)
        {
            var groupMembers = new List<UserGlobalView>();
            foreach (var dbGU in dbGroupUsers)
            {
                var gMember = dbGU.User;
                groupMembers.Add(new UserGlobalView { Name = gMember.FirstName, Surname = gMember.LastName, Address = gMember.Email });
            }
            return groupMembers;
        }

        public void UpdateGroupName(GroupNameUpdate groupNameUpdate)
        {
            Group group = _context.Groups.Where(g => g.ID == groupNameUpdate.GroupID).FirstOrDefault();
            group.GroupName = groupNameUpdate.Name;
            _context.SaveChanges();
        }

        public void AddGroup(int ownerID, NewGroup newGroup)
        {
            User owner = _context.Users.Where(u => u.ID == ownerID).FirstOrDefault();
            Group createdGroup = new Group
            {
                Owner = owner,
                GroupName = newGroup.Name
            };
            _context.Groups.Add(createdGroup);
            _context.SaveChanges();
        }

        public void RemoveGroup(int groupID)
        {
            Group group = _context.Groups.Where(g => g.ID == groupID).FirstOrDefault();
            _context.Groups.Remove(group);
            _context.SaveChanges();
        }

        public void AddUserToGroup(GroupMemberUpdate groupMemberUpdate)
        {
            User user = _context.Users.Include(u => u.GroupUsers).Where(u => u.Email == groupMemberUpdate.GroupMemberAddress).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("GroupMemberAddress", new Exception("No such user address in database."));
            }
            if (user.GroupUsers.Where(gu => gu.GroupID == groupMemberUpdate.GroupID).Any())
            {
                throw new Exception("GroupMemberAddress", new Exception("This user is already in that group."));
            }
            GroupUser groupUser = new GroupUser
            {
                UserID = user.ID,
                GroupID = groupMemberUpdate.GroupID
            };
            _context.GroupUsers.Add(groupUser);
            _context.SaveChanges();
        }

        public void RemoveUserFromGroup(GroupMemberUpdate groupMemberUpdate)
        {
            GroupUser groupUser = _context.GroupUsers.Include(gu => gu.User).Where(gu => gu.GroupID == groupMemberUpdate.GroupID && gu.User.Email == groupMemberUpdate.GroupMemberAddress).First();
            _context.GroupUsers.Remove(groupUser);
            _context.SaveChanges();
        }
    }
}
