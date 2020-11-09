﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database.Configurations
{
    public class GroupUserEntityConfiguration : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.HasKey(c => new { c.GroupID, c.UserID });

            builder.HasOne(c => c.Group)
                .WithMany(c => c.GroupUsers)
                .HasForeignKey(c => c.GroupID);

            builder.HasOne(c => c.User)
                .WithMany(c => c.GroupUsers)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}