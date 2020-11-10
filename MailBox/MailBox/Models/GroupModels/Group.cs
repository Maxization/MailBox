using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class Group
    {
        public string Name { get; set; }

        public List<GroupMember> GroupMembers { get; set; }

        public Group(string name, List<GroupMember> groupMembers)
        {
            this.Name = name;
            this.GroupMembers = groupMembers;
        }
    }
}
