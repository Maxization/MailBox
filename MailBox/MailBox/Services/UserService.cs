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
    }
}
