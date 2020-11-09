using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database.Configurations
{
    public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.GroupName)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne(c => c.Owner)
                .WithMany(c => c.Groups)
                .IsRequired();
        }
    }
}
