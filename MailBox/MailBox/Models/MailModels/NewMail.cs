
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MailBox.Models.MailModels
{
    public class NewMail
    {
        public List<string> CCRecipientsAddresses { get; set; }
        public List<string> BCCRecipientsAddresses { get; set; }
        public string Topic { get; set; }
        public string Text { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
