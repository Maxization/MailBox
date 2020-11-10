using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class InboxMail
    {
        public int Id { get; }
        public bool Read { get; set; }
        public Sender Sender { get; }
        public List<Recipient> Recipients { get; }
        public string Topic { get; }
        public string Text { get; }
        public DateTime Date { get; }
        public InboxMail MailReply { get; }

        public InboxMail(int id, bool read, Sender sender, List<Recipient> recipients, string topic, string text, DateTime date, InboxMail mailReply)
        {
            this.Id = id;
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
