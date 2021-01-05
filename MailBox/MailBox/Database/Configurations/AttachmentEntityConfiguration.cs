
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailBox.Database.Configurations
{
    public class AttachemtEntityConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(c => new { c.ID });

            builder.Property(c => c.MailID)
                .IsRequired();

            builder.Property(c => c.Filename)
                .IsRequired();

            builder.HasOne(c => c.Mail)
                .WithMany(c => c.Attachments)
                .HasForeignKey(c => c.MailID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.MailID);
        }
    }
}
