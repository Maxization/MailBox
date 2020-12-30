
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailBox.Database.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(c => c.Email)
                .IsUnique();

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
                .WithOne(c => c.Sender)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Groups)
                .WithOne(c => c.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Role)
                .WithMany(c => c.Users)
                .IsRequired();
            
        }
    }
}
