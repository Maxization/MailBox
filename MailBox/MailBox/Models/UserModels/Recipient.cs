using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models.UserModels
{
    public class Recipient
    {
        public string Address { get; }

        public Recipient(string address)
        {
            this.Address = address;
        }
    }
}
