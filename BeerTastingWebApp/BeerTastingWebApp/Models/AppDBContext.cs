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
        
        public DbSet<Beer> Beer { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tasting> Tasting { get; set; }

    } 
    
}
