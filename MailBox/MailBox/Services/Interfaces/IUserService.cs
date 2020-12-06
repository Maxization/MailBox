
using System.Collections.Generic;
using MailBox.Models.UserModels;

namespace MailBox.Services
{
    public interface IUserService
    {
        List<UserGlobalView> GetGlobalContactList();
        List<UserAdminView> GetAdminViewList();

        void SetUserRole(UserRoleUpdate userRoleUpdate);

        void SetUserEnableStatus(UserEnableUpdate userEnableUpdate);
    }
}
