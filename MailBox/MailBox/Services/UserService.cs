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
        public List<UserGlobalView> GetGlobalContactList()
        {
            var dbUsers = context.Users.AsQueryable();
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

            var userMails = context.UserMails.Include(x => x.User).ToList();
            foreach(UserMail um in userMails)
            {
                if(!um.Read && !addedUsers.Contains(um.User.Email))
                {
                    addedUsers.Add(um.User.Email);
                    result.Add(new UserNotification { Name = um.User.FirstName + " " + um.User.LastName, Email = um.User.Email });
                }
            }
            
            return result;
        }
    }
}
