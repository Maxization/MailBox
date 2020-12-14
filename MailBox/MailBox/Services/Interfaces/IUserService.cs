
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace MailBox.Services
{
    public interface IUserService
    {
        public List<UserEmailNotification> GetUsersEmailWithUnreadMails();
        public List<UserSMSNotification> GetUsersNumberWithUnreadMails();
        public List<UserGlobalView> GetGlobalContactList();
        public List<UserAdminView> GetAdminViewList();
        void UpdateUserRole(UserRoleUpdate userRoleUpdate);
        void RemoveUser(DeletedUser deletedUser);
    }
}
