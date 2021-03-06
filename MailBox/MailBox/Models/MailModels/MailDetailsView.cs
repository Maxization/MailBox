
using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;

namespace MailBox.Models.MailModels
{
    public class MailDetailsView
    {
        public int MailID { get; set; }
        public bool Read { get; set; }
        public UserGlobalView Sender { get; set; }
        public List<string> RecipientsAddresses { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public List<(string filename, Guid id)> Attachments { get; set; }
    }
}