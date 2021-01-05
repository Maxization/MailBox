
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.HasOne(c => c.Sender)
                .WithMany(c => c.CreatedMails);
        }
    }
}
