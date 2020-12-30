
﻿using MailBox.Models.UserModels;
using System;

namespace MailBox.Models.MailModels
{
    public class MailInboxView
    {
        public int MailID { get; set; }
        public bool Read { get; set; }
        public UserGlobalView Sender { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
    }
}
