using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class NewMail
    {
        public int SenderId { get; set; }
        public List<string> CCRecipientsAddresses { get; set; }
        public List<string> BCCRecipientsAddresses { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public NewMail( int senderId, 
                        List<string> CCRecipientsAddresses, 
                        List<string> BCCRecipientsAddresses, 
                        string topic, 
                        string text,
                        DateTime date)
        {
            this.SenderId = senderId;
            this.CCRecipientsAddresses = CCRecipientsAddresses;
            this.BCCRecipientsAddresses = BCCRecipientsAddresses;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
        }
    }
}
