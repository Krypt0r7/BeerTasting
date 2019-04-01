using BeerSession.Data;
using BeerSession.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Hubs
{
    public class TastingHub : Hub
    {
        private readonly ApplicationDbContext dbContext;

        public TastingHub(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task NewParticipant(string name, string email, string tastingId)
        {
            var tastingObject = dbContext.Tasting.First(z => z.TastingTag.ToString() == tastingId);
            var newPart = new Participant
            {
                Name = name,
                Email = email,
                Tasting = tastingObject
            };

            dbContext.Add(newPart);
            await dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("GetParticipant", name, email);
        }

        public async Task RemoveThePart(string name, string tastingId)
        {
            var tastingObject = dbContext.Tasting.First(z => z.TastingTag.ToString() == tastingId);

            foreach (var item in tastingObject.Participants)
            {
                if (item.Name == name)
                {
                    dbContext.Remove(item);
                    await dbContext.SaveChangesAsync();
                    await Clients.All.SendAsync("RemoveParticipant", name);
                }
            }
        }
    }
}
