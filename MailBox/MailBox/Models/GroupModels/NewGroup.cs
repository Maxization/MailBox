using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models.GroupModels
{
    public class NewGroup
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public NewGroup(int ownerId, string name)
        {
            this.OwnerId = ownerId;
            this.Name = name;
        }
    }
}
