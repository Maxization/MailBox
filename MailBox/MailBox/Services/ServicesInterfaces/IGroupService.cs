
using System.Collections.Generic;
using MailBox.Models.GroupModels;

namespace MailBox.Services.ServicesInterfaces
{
    public interface IGroupService
    {
        public List<GroupView> GetUserGroupsList(int userID);
    }
}
