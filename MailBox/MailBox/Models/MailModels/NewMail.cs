using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class NewMail
    {
        public List<string> CCRecipientsAddresses { get; set; }
        public List<string> BCCRecipientsAddresses { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
