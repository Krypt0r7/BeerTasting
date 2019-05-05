using BeerSession.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Data.BeerService
{
    public class BeerGetter : IBeerGetter
    {
        private readonly ApplicationDbContext dbContext;
        public BeerGetter(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Beer> GetBeerAsync(string response, string tastingId)
        {
            var tastingObject = dbContext.Tasting.FirstOrDefault(t => t.TastingTag.ToString() == tastingId);
            var beer = JsonConvert.DeserializeObject<ViewModels.Systemet>(response);

            Beer newBeer = new Beer
            {
                Producer = beer.Producent,
                Country = beer.Land,
                Price = beer.PrisInkMoms,
                SystemetNumber = beer.ArtikelId,
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

            return newBeer;
        }

       
    }
}
