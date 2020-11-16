
using System;
using System.Collections.Generic;

namespace MailBox.Models.MailModels
{
    public class NewMail
    {
        public int SenderID { get; set; }
        public List<string> CCRecipientsAddresses { get; set; }
        public List<string> BCCRecipientsAddresses { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public NewMail( int senderID, 
                        List<string> CCRecipientsAddresses, 
                        List<string> BCCRecipientsAddresses, 
                        string topic, 
                        string text,
                        DateTime date)
        {
            this.SenderID = senderID;
            this.CCRecipientsAddresses = CCRecipientsAddresses;
            this.BCCRecipientsAddresses = BCCRecipientsAddresses;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
        }
    }
}
