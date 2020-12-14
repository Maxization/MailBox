﻿
using MailBox.Database;
using MailBox.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailBox.Services
{
    public class UserService : IUserService
    {
        private readonly MailBoxDBContext _context;

        public UserService(MailBoxDBContext context)
        {
            _context = context;
        }

        public List<UserAdminView> GetAdminViewList()
        {
            var dbUsers = _context.Users.Include(u => u.Role).AsQueryable();
            List<UserAdminView> users = new List<UserAdminView>();
            foreach (var dbUser in dbUsers)
            {
                users.Add(new UserAdminView
                {
                    Name = dbUser.FirstName,
                    Surname = dbUser.LastName,
                    Address = dbUser.Email,
                    RoleName = dbUser.Role.RoleName
                });
            }
            return users;
        }

        public List<UserGlobalView> GetGlobalContactList()
        {
            var dbUsers = _context.Users.Include(u => u.Role).Where(u => u.Role.RoleName == "User" || u.Role.RoleName == "Admin").AsQueryable();
            List<UserGlobalView> users = new List<UserGlobalView>();
            foreach (var dbUser in dbUsers)
            {
                users.Add(new UserGlobalView
                {
                    Name = dbUser.FirstName,
                    Surname = dbUser.LastName,
                    Address = dbUser.Email
                });
            }
            return users;
        }

        public List<UserNotification> GetUsersAndNumberOfUnreadMails()
        {
            List<UserNotification> result = new List<UserNotification>();
            List<string> addedUsers = new List<string>();

            var userMails = _context.UserMails.Include(x => x.User).ToList();
            foreach (UserMail um in userMails)
            {
                if (!um.Read && !addedUsers.Contains(um.User.Email))
                {
                    addedUsers.Add(um.User.Email);
                    result.Add(new UserNotification { Name = um.User.FirstName + " " + um.User.LastName, Email = um.User.Email });
                }
            }

            return result;
        }

        public void UpdateUserRole(UserRoleUpdate userRoleUpdate)
        {
            var user = _context.Users.First(u => u.Email == userRoleUpdate.Address);
            user.Role = _context.Roles.First(r => r.RoleName == userRoleUpdate.RoleName);
            _context.SaveChanges();
        }

        public void RemoveUser(DeletedUser deletedUser)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = _context.Users.Include(u => u.GroupUsers).Include(u => u.UserMails).First(u => u.Email == deletedUser.Address);
                foreach (var groupUser in user.GroupUsers)
                {
                    _context.GroupUsers.Remove(groupUser);
                }
                foreach (var userMail in user.UserMails)
                {
                    _context.UserMails.Remove(userMail);
                }
                _context.Users.Remove(user);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }
    }
}
