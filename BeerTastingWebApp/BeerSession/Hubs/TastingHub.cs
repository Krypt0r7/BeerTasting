using BeerSession.Data;
using BeerSession.Data.BeerService;
using BeerSession.Data.ParticipantService;
using BeerSession.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BeerSession.Hubs
{
    public class TastingHub : Hub
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IBeerGetter beerGetter;
        private readonly IParticipantTools participantTools;

        public TastingHub(ApplicationDbContext _dbContext, IBeerGetter _beerGetter, IParticipantTools _participantTools)
        {
            beerGetter = _beerGetter;
            dbContext = _dbContext;
            participantTools = _participantTools;
        }

        public override Task OnConnectedAsync()
        {
            var userDB = dbContext.User.Include(i => i.Tastings).ThenInclude(t => t.Tasting).FirstOrDefault(u => u.UserName == Context.User.Identity.Name);

            if (userDB.Tastings.Count > 0)
            {
                foreach (var item in userDB.Tastings)
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, item.Tasting.TastingTag.ToString());
                }
            }
            return base.OnConnectedAsync();
        }

        public async Task NewParticipant(string name, string email, string tastingId)
        {
            await participantTools.GetParticipantAsync(tastingId, email, name);
            await Clients.Caller.SendAsync("GetParticipant", name, email);
        }

        public async Task CreateRoom(string tastingId)
        { 
            await Groups.AddToGroupAsync(Context.ConnectionId, tastingId);
        }

        public async Task RemoveThePart(string name, string tastingId)
        {
            await participantTools.RemoveParticipant(tastingId, name);
            await Clients.Caller.SendAsync("RemoveParticipant", name);
        }

        public async Task RemoveSelectedBeer(int id, string tastingId)
        {
            var theBeer = await dbContext.Beer.FirstAsync(f => f.ID == id);

            if (theBeer != null)
            {
                dbContext.Remove(theBeer);
                await dbContext.SaveChangesAsync();
                await Clients.Group(tastingId).SendAsync("removeBeer", id);
            }
        }

        public async Task GetMeSomeProducers()
        {
            using(var client = new HttpClient())
            {
                string response = await client.GetStringAsync("http://localhost:5000/api/systemet/producers/");

                await Clients.Caller.SendAsync("populateProducers", response);
            }
        }

        public async Task GetAllBeersFromProducer(string producer)
        {
            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync($"http://localhost:5000/api/systemet/producers/{producer}/");

                await Clients.Caller.SendAsync("populateBeers", response);
            }
        }

        public async Task GetBeerInfo(string name, string tastingId)
        {
            string[] split = name.Split(',').Select(s => s.Trim()).ToArray();
            string response = "";
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync($"http://localhost:5000/api/systemet/beer/{split[0]}");
            }
            var newBeer = await beerGetter.GetBeerAsync(response, tastingId);

            await Clients.Caller.SendAsync("GetBeer", newBeer.Name, newBeer.Producer, newBeer.Country, newBeer.Price, newBeer.SystemetNumber, newBeer.Alchohol, newBeer.ID);
        }

        public async Task NewBeer(int sysnum,string tastingId)
        {
            var tastingObject = dbContext.Tasting.First(z => z.TastingTag.ToString() == tastingId);
            string response;
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync($"http://localhost:5000/api/systemet/beer/{sysnum}");
            }
            var newBeer = await beerGetter.GetBeerAsync(response, tastingId);

            await Clients.Caller.SendAsync("GetBeer", newBeer.Name, newBeer.Producer, newBeer.Country, newBeer.Price, sysnum, newBeer.Alchohol, newBeer.ID);
        }
    }
}
