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

        public InboxMail(bool read, User sender, List<User> recipients, string topic, string text, DateTime date, InboxMail mailReply)
        {
            this.Read = read;
            this.Sender = sender;
            this.Recipients = recipients;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
            this.MailReply = mailReply;
        }
    }
}
