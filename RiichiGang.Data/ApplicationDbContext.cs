using Microsoft.EntityFrameworkCore;
using RiichiGang.Domain;

namespace RiichiGang.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Ruleset> Rulesets { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Club
            modelBuilder.Entity<Club>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Creator);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications);

            // Ruleset
            modelBuilder.Entity<Ruleset>()
                .HasIndex(r => new
                {
                    ClubId = r.ClubId,
                    Name = r.Name
                })
                .IsUnique();

            // User
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Stats);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}