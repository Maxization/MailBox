using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class NewMail
    {
        public User Sender { get; set; }
        public List<User> CCRecipients { get; set; }
        public List<User> BCRecipients { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public InboxMail MailReply { get; }
        public NewMail(User Sender, List<User> CCRecipients, List<User> BCRecipients, string Topic, string Text, DateTime Date, InboxMail MailReply)
        {
            this.Sender = Sender;
            this.CCRecipients = CCRecipients;
            this.BCRecipients = BCRecipients;
            this.Topic = Topic;
            this.Text = Text;
            this.Date = Date;
            this.MailReply = MailReply;
        }
    }
}
