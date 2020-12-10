
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace MailBox.Services
{
    public interface IUserService
    {
        List<UserGlobalView> GetGlobalContactList();
        List<UserAdminView> GetAdminViewList();

        void UpdateUserRole(UserRoleUpdate userRoleUpdate);

        void RemoveUser(DeletedUser deletedUser);
    }
}
