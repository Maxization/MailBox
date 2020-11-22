using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models.MailModels
{
    public class MailReadUpdate
    {
        public int MailId { get; set; }
        public bool Read { get; set; }
    }
}
