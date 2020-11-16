
using MailBox.Models.UserModels;
using System.Collections.Generic;

namespace MailBox.Models.GroupModels
{
    public class GroupView
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public List<UserGlobalView> GroupMembers { get; set; }

        public GroupView(int groupID, string name, List<UserGlobalView> groupMembers)
        {
            this.GroupID = groupID;
            this.Name = name;
            this.GroupMembers = groupMembers;
        }
    }
}
