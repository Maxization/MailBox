using MailBox.Models.UserModels;
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
        public Sender Sender { get; }
        public List<Recipient> Recipients { get; }
        public string Topic { get; }
        public string Text { get; }
        public DateTime Date { get; }
        public InboxMail MailReply { get; }

        public InboxMail(bool read, Sender sender, List<Recipient> recipients, string topic, string text, DateTime date, InboxMail mailReply)
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
