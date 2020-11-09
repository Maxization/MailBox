using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database
{
    public class Group
    {
        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public User Owner { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }

    }
}
