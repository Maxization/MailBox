using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public Role Role { get; set; }

        public User(string name, string surname, Role role)
        {
            this.Name = name;
            this.Surname = surname;
            this.Role = role;
        }
    }
}
