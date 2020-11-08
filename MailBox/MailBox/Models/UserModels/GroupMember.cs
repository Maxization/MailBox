using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class GroupMember
    {
        public string Name { get; }

        public string Surname { get; }

        public string Address { get; }

        public GroupMember(string name, string surname, string address)
        {
            this.Name = name;
            this.Surname = surname;
            this.Address = address;
        }
    }
}
