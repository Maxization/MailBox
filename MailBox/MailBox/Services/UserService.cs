using MailBox.Database;
using MailBox.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            var dbUsers = context.Users.Include(u => u.Role).AsQueryable();
            List<UserAdminView> users = new List<UserAdminView>();
            foreach (var dbUser in dbUsers)
            {
                if(dbUser.Role != null)
                users.Add(new UserAdminView
                {
                    Name = dbUser.FirstName,
                    Surname = dbUser.LastName,
                    Address = dbUser.Email,
                    RoleName = dbUser.Role.RoleName,
                    Enable = EnableStatusFromRole(dbUser.Role.RoleName)
                });
                else
                {
                    users.Add(new UserAdminView
                    {
                        Name = dbUser.FirstName,
                        Surname = dbUser.LastName,
                        Address = dbUser.Email,
                        RoleName = "Banned",
                        Enable = false
                    });
                }
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
                if (dbUser.Role == null || dbUser.Role.RoleName == "User" || dbUser.Role.RoleName == "Admin")
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

        public void SetUserRole(UserRoleUpdate userRoleUpdate)
        {
            var user = context.Users.First(u => u.Email == userRoleUpdate.Address);
            user.Role = context.Roles.First(r => r.RoleName == userRoleUpdate.RoleName);
            context.SaveChanges();
        }

        public void DeleteUser(DeletedUser deletedUser)
        {
            var user = context.Users.First(u => u.Email == deletedUser.Address);
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
