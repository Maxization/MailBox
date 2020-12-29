
using System.Collections.Generic;

namespace MailBox.Models.MailModels
{
    public class PagingMailInboxView
    {
        public List<MailInboxView> Mails { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
    }
}
