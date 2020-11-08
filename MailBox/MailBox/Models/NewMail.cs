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
        public NewMail(User sender, List<User> CCrecipients, List<User> BCrecipients, string topic, string text, DateTime date, InboxMail mailReply)
        {
            this.Sender = sender;
            this.CCRecipients = CCrecipients;
            this.BCRecipients = BCrecipients;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
            this.MailReply = mailReply;
        }
    }
}
