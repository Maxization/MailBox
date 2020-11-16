
using System.Linq;
using System.Collections.Generic;
using MailBox.Models.GroupModels;
using MailBox.Models.UserModels;
using MailBox.Database;
using MailBox.Services.ServicesInterfaces;
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
                groups.Add(new GroupView(dbG.ID, dbG.GroupName, GetGroupMembers(dbGroupUsers.ToList())));
            }
            return groups;
        }

        public List<UserGlobalView> GetGroupMembers(List<GroupUser> dbGroupUsers)
        {
            var groupMembers = new List<UserGlobalView>();
            foreach (var dbGU in dbGroupUsers)
            {
                var gMember = context.Users.Where(u => u.ID == dbGU.UserID).First();
                groupMembers.Add(new UserGlobalView(gMember.FirstName, gMember.LastName, gMember.Email));
            }
            return groupMembers;
        }
    }
}
