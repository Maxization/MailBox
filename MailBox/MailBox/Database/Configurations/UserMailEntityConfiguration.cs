
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailBox.Database.Configurations
{
    public class UserMailEntityConfiguration : IEntityTypeConfiguration<UserMail>
    {
        public void Configure(EntityTypeBuilder<UserMail> builder)
        {
            builder.HasKey(c => new { c.UserID, c.MailID });

            builder.Property(c => c.Read)
                .IsRequired();

            builder.Property(c => c.RecipientType)
                .IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(c => c.UserMails)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Mail)
                .WithMany(c => c.UserMails)
                .HasForeignKey(c => c.MailID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
