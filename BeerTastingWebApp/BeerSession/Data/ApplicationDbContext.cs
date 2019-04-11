using BeerSession.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeerSession.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTasting>().HasKey(sc => new { sc.TastingId, sc.UserId });

            builder.Entity<UserTasting>().HasOne(us => us.User).WithMany(t => t.Tastings).HasForeignKey(f => f.UserId);

            builder.Entity<UserTasting>().HasOne(us => us.Tasting).WithMany(t => t.Users).HasForeignKey(f => f.TastingId);

            builder.Entity<Tasting>().Property("DateCreated").HasDefaultValueSql("getutcdate()");

            builder.Entity<User>().Property("SignedUp").HasDefaultValueSql("getutcdate()");
        }

        public DbSet<Beer> Beer { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tasting> Tasting { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<UserTasting> UserTasting { get; set; }
        public DbSet<Connection> Connection { get; set; }
        public DbSet<TastingRoom> TastingRoom { get; set; }
    }
}
