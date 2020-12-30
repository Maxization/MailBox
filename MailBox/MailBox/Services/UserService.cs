
using MailBox.Database;
using MailBox.Models.NotificationModel;
using MailBox.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                    result.Add(new UserSMSNotification { Name = um.User.FirstName + " " + um.User.LastName, PhoneNumber=um.User.PhoneNumber });
                }
            }

            return result;
        }

        private async void SendNotificationToUser(string userEmail, string contentMes)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-api-key", "78b06e67-bda7-48e5-a032-12132f76eca1");
                Notification notification = new Notification
                {
                    Content = contentMes,
                    RecipientsList = new List<string>{ userEmail }.ToArray(),
                    WithAttachments = false
                };
                string json = await Task.Run(() => JsonConvert.SerializeObject(notification));
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://mini-notification-service.azurewebsites.net/notifications", content);

                var responseString = await response.Content.ReadAsStringAsync();
            }
        }

        public void UpdateUserRole(UserRoleUpdate userRoleUpdate)
        {
            var user = _context.Users.First(u => u.Email == userRoleUpdate.Address);
            user.Role = _context.Roles.First(r => r.RoleName == userRoleUpdate.RoleName);
            _context.SaveChanges();
            if(userRoleUpdate.RoleName == "User" || userRoleUpdate.RoleName == "Admin")
            {
                Task SendNotification = Task.Run(() => SendNotificationToUser(userRoleUpdate.Address, "ActivatedAccount"));
                SendNotification.Wait();
            }
            else if (userRoleUpdate.RoleName == "Banned")
            {
                Task SendNotification = Task.Run(() => SendNotificationToUser(userRoleUpdate.Address, "BannedAccount"));
                SendNotification.Wait();
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
