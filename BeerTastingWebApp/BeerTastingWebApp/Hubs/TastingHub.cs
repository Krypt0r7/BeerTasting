using BeerTastingWebApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerTastingWebApp.Hubs
{
    public class TastingHub : Hub
    {
        private readonly AppDBContext dbContext;

        public TastingHub(AppDBContext _dbContext)
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
