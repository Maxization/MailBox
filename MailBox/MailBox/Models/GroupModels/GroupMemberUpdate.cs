using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models.GroupModels
{
    public class GroupMemberUpdate
    {
        public int GroupId { get; set; }
        public string GroupMemberAddress { get; set; }
    }
}
