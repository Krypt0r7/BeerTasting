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

            modelBuilder.Entity<Tasting>().Property("DateCreated").HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<User>().Property("SignedUp").HasDefaultValueSql("getutcnow()");
        }

        public DbSet<Beer> Beer { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tasting> Tasting { get; set; }
        public DbSet<UserTasting> UserTasting { get; set; }
    } 
}
