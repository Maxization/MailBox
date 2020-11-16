﻿
namespace MailBox.Models.UserModels
{
    public class UserRoleUpdate
    {
        public string Address { get; set; }
        public Role Role { get; set; }

        public UserRoleUpdate(string address, Role role)
        {
            this.Address = address;
            this.Role = role;
        }
    }
}
