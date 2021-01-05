
using MailBox.Database;
using MailBox.Models.UserModels;
using MailBox.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailBox.Services
{
    public class UserService : IUserService
    {
        private readonly MailBoxDBContext _context;
        private readonly INotificationService _notificationService;

        public UserService(MailBoxDBContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
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

        public List<UserEmailNotification> GetUsersEmailWithUnreadMails()
        {
            List<UserEmailNotification> result = new List<UserEmailNotification>();
            List<string> addedUsers = new List<string>();

            var userMails = _context.UserMails.Include(x => x.User).ToList();
            foreach (UserMail um in userMails)
            {
                if (!um.Read && !addedUsers.Contains(um.User.Email))
                {
                    addedUsers.Add(um.User.Email);
                    result.Add(new UserEmailNotification { Name = um.User.FirstName + " " + um.User.LastName, Email = um.User.Email });
                }
            }

            return result;
        }

        public List<UserSMSNotification> GetUsersNumberWithUnreadMails()
        {
            List<UserSMSNotification> result = new List<UserSMSNotification>();
            List<string> addedUsers = new List<string>();

            var userMails = _context.UserMails.Include(x => x.User).ToList();
            foreach (UserMail um in userMails)
            {
                if (!um.Read && !addedUsers.Contains(um.User.Email))
                {
                    addedUsers.Add(um.User.Email);
                    result.Add(new UserSMSNotification { Name = um.User.FirstName + " " + um.User.LastName, PhoneNumber = um.User.PhoneNumber });
                }
            }

            return result;
        }



        public void UpdateUserRole(UserRoleUpdate userRoleUpdate)
        {
            var user = _context.Users.First(u => u.Email == userRoleUpdate.Address);
            user.Role = _context.Roles.First(r => r.RoleName == userRoleUpdate.RoleName);
            _context.SaveChanges();
            if (userRoleUpdate.RoleName == "User" || userRoleUpdate.RoleName == "Admin")
            {
                _notificationService.SendNotification(new List<string> { userRoleUpdate.Address }, "ActivatedAccount", false);
            }
            else if (userRoleUpdate.RoleName == "Banned")
            {
                _notificationService.SendNotification(new List<string> { userRoleUpdate.Address }, "BannedAccount", false);
            }
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
