
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace MailBox.Services
{
    public interface IUserService
    {
        public List<UserGlobalView> GetGlobalContactList();

        public List<UserNotification> GetUsersAndNumberOfUnreadMails();
    }
}
