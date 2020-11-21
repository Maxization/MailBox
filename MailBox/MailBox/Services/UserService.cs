using MailBox.Database;
using MailBox.Models.UserModels;
using MailBox.Services.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Services
{
    public class UserService : IUserService
    {
        private readonly MailBoxDBContext context;
        private IConfiguration configuration;

        public UserService(MailBoxDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public List<UserGlobalView> GetGlobalContactList()
        {
            var dbUsers = context.Users.AsQueryable();
            List<UserGlobalView> users = new List<UserGlobalView>();
            foreach(var dbUser in dbUsers)
            {
                users.Add(new UserGlobalView(dbUser.FirstName, dbUser.LastName, dbUser.Email));
            }
            return users;
        }
    }
}
