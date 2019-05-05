using System.Threading.Tasks;
using BeerSession.Models;

namespace BeerSession.Data.BeerService
{
    public interface IBeerGetter
    {
        Task<Beer> GetBeerAsync(string response, string tastingId);
    }
}