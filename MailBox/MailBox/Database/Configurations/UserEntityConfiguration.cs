using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database.Configurations
{
    public class UserEntityConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.ID);
            
            builder.Property(c => c.FirstName)
                .HasMaxLength(30)
                .IsRequired();
            
            builder.Property(c => c.LastName)
                .HasMaxLength(30);
            
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasMany(c => c.CreatedMails)
                .WithOne(c => c.Sender);

            builder.HasMany(c => c.Groups)
                .WithOne(c => c.Owner);

            builder.HasOne(c => c.Role)
                .WithMany(c => c.Users)
                .IsRequired();
        }
    }
}
