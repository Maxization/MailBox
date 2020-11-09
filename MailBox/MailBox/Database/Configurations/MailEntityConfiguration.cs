using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBox.Database.Configurations
{
    public class MailEntityConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Text)
                .HasMaxLength(3000)
                .IsRequired();

            builder.Property(c => c.Topic)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Read)
                .IsRequired();

            builder.HasMany(c => c.Mails)
                .WithOne(c => c.MailReply);

            builder.HasOne(c => c.MailReply)
                .WithMany(c => c.Mails);

            builder.HasOne(c => c.Sender)
                .WithMany(c => c.CreatedMails);
        }
    }
}
