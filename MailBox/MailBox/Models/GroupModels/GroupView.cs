﻿using MailBox.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Models
{
    public class GroupView
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<UserGlobalView> GroupMembers { get; set; }
        public GroupView(int groupId, string name, List<UserGlobalView> groupMembers)
        {
            this.GroupId = groupId;
            this.Name = name;
            this.GroupMembers = groupMembers;
        }
    }
}
