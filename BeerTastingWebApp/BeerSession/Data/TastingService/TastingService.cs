using BeerSession.Data.TastingService;
using BeerSession.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Data.TastingService
{
    public class TastingService : ITastingService 
    {
        private readonly ApplicationDbContext dbContext;

        public TastingService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Tasting> CreateTastingAsync(IdentityUser user, Tasting tasting)
        {
            tasting.TastingTag = Guid.NewGuid();
            tasting.SessionMeister = dbContext.User.First(x => x.UserIdentity == user.Id);

            dbContext.Add(tasting);
            await dbContext.SaveChangesAsync();

            return tasting;
        }

        public async Task RemoveTasting(string tastingId)
        {
            var tasting = dbContext.Tasting.Include(i => i.Participants).Include(t => t.Beers).First(f => f.TastingTag.ToString() == tastingId);
            foreach (var item in tasting.Beers)
            {
                dbContext.Remove(item);
            }
            
            foreach (var item in tasting.Participants)
            {
                dbContext.Remove(item);
            }
            dbContext.Remove(tasting);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(IdentityUser user)
        {
            var userDb = dbContext.User.AsNoTracking().Include(i => i.MeisterTastings).Include(i => i.Tastings).ThenInclude(t => t.Tasting).FirstOrDefault(f => f.UserIdentity == user.Id);
            // var userDb = await dbContext.User.Include(i => i.Tastings).ThenInclude(i => i.Tasting).Include(i => i.MeisterTastings).FirstOrDefaultAsync(f => f.UserIdentity == user.Id);
            if (userDb == null)
            {
                var newUser = new User
                {
                    UserName = user.UserName,
                    Name = user.UserName,
                    Email = user.Email,
                    UserIdentity = user.Id
                };

                dbContext.Add(newUser);
                await dbContext.SaveChangesAsync();

                userDb = dbContext.User.AsNoTracking().Include(i => i.Tastings).ThenInclude(i => i.Tasting).Include(i => i.MeisterTastings).FirstOrDefault(f => f.UserIdentity == user.Id);
            }

            return userDb;
        }

  
    }
}
