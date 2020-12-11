
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace MailBox.Services
{
    public interface IUserService
    {
        public List<UserNotification> GetUsersAndNumberOfUnreadMails();
        public List<UserGlobalView> GetGlobalContactList();
        public List<UserAdminView> GetAdminViewList();
        void UpdateUserRole(UserRoleUpdate userRoleUpdate);
        void RemoveUser(DeletedUser deletedUser);
    }
}
