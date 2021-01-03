
using System;

namespace MailBox.Database
{
    public class Attachment
    {
        public Guid ID { get; set; }
        public int MailID { get; set; }
        public Mail Mail { get; set; }
        public string Filename { get; set; }
    }
}
