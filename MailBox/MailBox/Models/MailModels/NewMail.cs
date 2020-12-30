
using System;
using System.Collections.Generic;

namespace MailBox.Models.MailModels
{
    public class NewMail
    {
        public List<string> CCRecipientsAddresses { get; set; }
        public List<string> BCCRecipientsAddresses { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
    }
}
