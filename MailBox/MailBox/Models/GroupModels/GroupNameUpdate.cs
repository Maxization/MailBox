using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models.GroupModels
{
    public class GroupNameUpdate
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public GroupNameUpdate(int groupId, string newName)
        {
            this.GroupId = groupId;
            this.Name = newName;
        }
    }
}
