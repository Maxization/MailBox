using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    [Serializable]
    public class InboxMail
    {
        public bool Read { get; set; }
        public User Sender { get; }
        public List<User> Recipients { get; }
        public string Topic { get; }
        public string Text { get; }
        public DateTime Date { get; }
        public InboxMail MailReply { get; }

        public InboxMail(bool Read, User Sender, List<User> Recipients, string Topic, string Text, DateTime date, InboxMail MailReply)
        {
            this.Read = Read;
            this.Sender = Sender;
            this.Recipients = Recipients;
            this.Topic = Topic;
            this.Text = Text;
            this.Date = Date;
            this.MailReply = MailReply;
        }
    }
}
