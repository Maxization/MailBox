using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class User
    {
        public string Name { get; }

        public string Surname { get; }

        public string Address { get; }

        public Role Role { get; set; }

        public User(string name, string surname, string address, Role role)
        {
            this.Name = name;
            this.Surname = surname;
            this.Address = address;
            this.Role = role;
        }
    }
}
