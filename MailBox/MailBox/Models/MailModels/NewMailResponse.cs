
using System;

namespace MailBox.Models.MailModels
{
    public class NewMailResponse
    {
        public int SenderID { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int MailReplyID { get; set; }

        public NewMailResponse(int senderID,
                        string topic,
                        string text,
                        DateTime date,
                        int mailReplyID)
        {
            this.SenderID = senderID;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
            this.MailReplyID = mailReplyID;
        }
    }
}
