using BeerSession.Data;
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

        public TastingHub(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
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

        public async Task CreateRoom(string tastingId)
        { 
            var room = dbContext.TastingRoom.FirstOrDefault(s => s.TastingId.ToString() == tastingId);
            if (room == null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tastingId);
            }
        }

        public async Task RemoveThePart(string name, string tastingId)
        {
            var tastingObject = dbContext.Tasting.Include(i => i.Participants).First(z => z.TastingTag.ToString() == tastingId);

            var participant = tastingObject.Participants.First(p => p.Name == name);

            if (participant != null)
            {
                dbContext.Remove(participant);
                await dbContext.SaveChangesAsync();
                await Clients.All.SendAsync("RemoveParticipant", name);
            }
        }

        public async Task RemoveSelectedBeer(int id)
        {
            var theBeer = await dbContext.Beer.FirstAsync(f => f.ID == id);

            if (theBeer != null)
            {
                dbContext.Remove(theBeer);
                await dbContext.SaveChangesAsync();
                await Clients.All.SendAsync("removeBeer", id);
            }
        }

        public async Task GetMeSomeProducers()
        {
            using(var client = new HttpClient())
            {
                string response = await client.GetStringAsync("http://localhost:5000/api/systemet/producers/");

                await Clients.All.SendAsync("populateProducers", response);
            }
        }

        public async Task GetAllBeersFromProducer(string producer)
        {
            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync($"http://localhost:5000/api/systemet/producers/{producer}/");

                await Clients.All.SendAsync("populateBeers", response);
            }
        }

        public async Task GetBeerInfo(string name)
        {
            string[] split = name.Split(',').Select(s => s.Trim()).ToArray();
            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync($"http://localhost:5000/api/systemet/beer/{split[0]}");
                await Clients.All.SendAsync("populateWithBeer", response);
            }
        }

        public async Task NewBeer(int sysnum,string tastingId)
        {
            var tastingObject = dbContext.Tasting.First(z => z.TastingTag.ToString() == tastingId);
            string response;
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync($"http://localhost:5000/api/systemet/beer/{sysnum}");
            }

            var beer = JsonConvert.DeserializeObject<ViewModels.Systemet>(response);

            Beer newBeer = new Beer
            {
                Producer = beer.Producent,
                Country = beer.Land,
                Price = beer.PrisInkMoms,
                SystemetNumber = sysnum,
                Alchohol = beer.Alkoholhalt,
                Tasting = tastingObject
            };

            if (beer.Producent.ToLower().Contains(beer.Namn.ToLower())) newBeer.Name = beer.Namn2;
            else if (beer.Producent.ToLower() == beer.Namn2.ToLower()) newBeer.Name = beer.Namn;
            else if (beer.Namn.ToLower().StartsWith(beer.Producent.ToLower()))
            {
                int length = beer.Producent.Length;
                string cleanName = beer.Namn.Substring(length + 1);
                newBeer.Name = cleanName;

            }
            else newBeer.Name = beer.Namn + ", " + beer.Namn2;

            await dbContext.AddAsync(newBeer);
            await dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("GetBeer", newBeer.Name, newBeer.Producer, newBeer.Country, newBeer.Price, sysnum, newBeer.Alchohol, beer.ID);
        }
    }
}
