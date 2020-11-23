
﻿using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;

namespace MailBox.Models
{
    public class MailInboxView
    {
        public int MailID { get; set; }
        public bool Read { get; set; }
        public UserGlobalView Sender { get; set; }
        public List<string> RecipientsAddresses  { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
