
using System.Collections.Generic;

namespace MailBox.Database
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Mail> CreatedMails { get; set; }
        public ICollection<UserMail> UserMails { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
