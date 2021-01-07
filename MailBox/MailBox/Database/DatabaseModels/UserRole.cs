
using System.Collections.Generic;

namespace MailBox.Database
{
    public class UserRole
    {
        public int ID { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
