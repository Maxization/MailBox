
using System.Collections.Generic;
using MailBox.Models.GroupModels;
using MailBox.Models.UserModels;

namespace MailBox.Services.ServicesInterfaces
{
    public interface IUserService
    {
        public List<UserGlobalView> GetGlobalContactList();
    }
}
