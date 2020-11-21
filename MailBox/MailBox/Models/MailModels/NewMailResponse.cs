using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models.MailModels
{
    public class NewMailResponse
    {
        public int SenderId { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int MailReplyId { get; set; }
        public NewMailResponse(int senderId,
                        string topic,
                        string text,
                        DateTime date,
                        int mailReplyId)
        {
            this.SenderId = senderId;
            this.Topic = topic;
            this.Text = text;
            this.Date = date;
            this.MailReplyId = mailReplyId;
        }
    }
}
