using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database
{
    public class UserRole
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
