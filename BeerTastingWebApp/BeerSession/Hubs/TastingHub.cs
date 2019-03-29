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

        public async Task NewParticipant(string name, string email)
        {
            var newPart = new Participant
            {
                Name = name,
                Email = email
            };

            dbContext.Add(newPart);
            await dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("GetParticipant", name, email);
        }
    }
}
