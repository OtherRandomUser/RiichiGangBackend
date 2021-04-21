using Microsoft.EntityFrameworkCore;
using RiichiGang.Domain;

namespace RiichiGang.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Stats);

            base.OnModelCreating(modelBuilder);
        }
    }
}