using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class NewMail
    {
        public Sender Sender { get; set; }
        public List<Recipient> CCRecipients { get; set; }
        public List<Recipient> BCCRecipients { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public InboxMail MailReply { get; set; }
        public NewMail(Sender sender, List<Recipient> CCrecipients, List<Recipient> BCCrecipients, string topic, string text, DateTime date, InboxMail mailReply)
        {
            this.Sender = sender;
            this.CCRecipients = CCrecipients;
            this.BCCRecipients = BCCrecipients;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
            this.MailReply = mailReply;
        }
    }
}
