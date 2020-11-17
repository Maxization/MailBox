using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class MailInboxView
    {
        public int MailId { get; set; }
        public bool Read { get; set; }
        public UserGlobalView Sender { get; set; }
        public List<string> RecipientsAddresses  { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public MailInboxView MailReply { get; set; }

        public MailInboxView(   int mailId, 
                                bool read, 
                                UserGlobalView sender, 
                                List<string> recipientsAddresses, 
                                string topic, 
                                string text, 
                                DateTime date, 
                                MailInboxView mailReply)
        {
            this.MailId = mailId;
            this.Read = read;
            this.Sender = sender;
            this.RecipientsAddresses = recipientsAddresses;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
            this.MailReply = mailReply;
        }
    }
}
