
using MailBox.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MailBox.Database
{
    public class MailBoxDBContext : DbContext
    {
        public MailBoxDBContext(DbContextOptions<MailBoxDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<UserMail> UserMails { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupUserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserMailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
        }
    }
}
