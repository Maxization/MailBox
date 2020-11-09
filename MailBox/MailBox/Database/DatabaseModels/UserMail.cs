using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database
{
    public enum RecipientType
    {
        CC,
        BCC
    }
    public class UserMail
    {
        public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid MailID { get; set; }
        public Mail Mail { get; set; }
        public RecipientType RecipientType { get; set; }
        public bool Read { get; set; }
        
    }
}
