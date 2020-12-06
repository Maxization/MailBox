using MailBox.Database;
using MailBox.Models.UserModels;
using System.Collections.Generic;

namespace MailBox.Services
{
    public class UserService : IUserService
    {
        private readonly MailBoxDBContext context;

        public UserService(MailBoxDBContext context)
        {
            this.context = context;
        }

        public List<UserAdminView> GetAdminViewList()
        {
            var dbUsers = context.Users.AsQueryable();
            List<UserAdminView> users = new List<UserAdminView>();
            foreach (var dbUser in dbUsers)
            {
                users.Add(new UserAdminView
                {
                    Name = dbUser.FirstName,
                    Surname = dbUser.LastName,
                    Address = dbUser.Email,
                    RoleName = dbUser.Role.RoleName,
                    Enable = EnableStatusFromRole(dbUser.Role.RoleName)
                });
            }
            return users;
        }

        private bool EnableStatusFromRole(string roleName)
        {
            bool enable = false;
            switch(roleName)
            {
                case "New": enable = false; break;
                case "Banned": enable = false; break;
                case "Admin": enable = true; break;
                case "User": enable = true; break;
            }
            return enable;
        }

        public List<UserGlobalView> GetGlobalContactList()
        {
            var dbUsers = context.Users.AsQueryable();
            List<UserGlobalView> users = new List<UserGlobalView>();
            foreach (var dbUser in dbUsers)
            {
                if (dbUser.Role.RoleName == "User" || dbUser.Role.RoleName == "Admin")
                { users.Add(new UserGlobalView
                {
                    Name = dbUser.FirstName,
                    Surname = dbUser.LastName,
                    Address = dbUser.Email
                });
                }
            }
            return users;
        }

        public void SetUserEnableStatus(UserEnableUpdate userEnableUpdate)
        {
            User user = context.Users.Find(userEnableUpdate.Address);
            if(userEnableUpdate.Enable)
            {
                user.Role = context.Roles.Find("User");
            }
            else
            {
                user.Role = context.Roles.Find("Banned");
            }
            context.SaveChanges();
        }

        public void SetUserRole(UserRoleUpdate userRoleUpdate)
        {
            User user = context.Users.Find(userRoleUpdate.Address);
            user.Role = context.Roles.Find(userRoleUpdate.RoleName);
            context.SaveChanges();
        }
    }
}
