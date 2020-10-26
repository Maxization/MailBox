using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    [Serializable]
    public class Mail
    {
        public bool Read { get; set; }
        public string Recipient { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Mail(bool read, string recipient, string text, DateTime date)
        {
            this.Read = read;
            this.Recipient = recipient;
            this.Text = text;
            this.Date = date;
        }
    }
}
