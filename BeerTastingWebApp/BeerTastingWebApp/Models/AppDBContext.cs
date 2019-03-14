using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTastingWebApp.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTasting>().HasKey(sc => new { sc.TastingId, sc.UserId });

            modelBuilder.Entity<UserTasting>().HasOne(us => us.User).WithMany(t => t.Tastings).HasForeignKey(f => f.UserId);

            modelBuilder.Entity<UserTasting>().HasOne(us => us.Tasting).WithMany(t => t.Users).HasForeignKey(f => f.TastingId);


            modelBuilder.Entity<Tasting>().Property("DateCreated").HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>().Property("SignedUp").HasDefaultValueSql("getdate()");
        }

        public DbSet<Beer> Beer { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tasting> Tasting { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<UserTasting> UserTasting { get; set; }
    } 
}
