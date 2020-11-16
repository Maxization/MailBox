﻿
namespace MailBox.Models.UserModels
{
    public class UserGlobalView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public UserGlobalView(string name, string surname, string address)
        {
            this.Name = name;
            this.Surname = surname;
            this.Address = address;
        }
    }
}