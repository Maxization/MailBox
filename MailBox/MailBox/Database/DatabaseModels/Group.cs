
using System.Collections.Generic;

namespace MailBox.Database
{
    public class Group
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public User Owner { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }

    }
}
