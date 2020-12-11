
using System;
using System.Collections.Generic;

namespace MailBox.Database
{
    public class Mail
    {
        public int ID { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User Sender { get; set; }
        public ICollection<UserMail> UserMails { get; set; }
    }
}
