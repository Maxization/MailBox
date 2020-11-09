using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database
{
    public class GroupUser
    {
        public Guid GroupID { get; set; }
        public Group Group { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
