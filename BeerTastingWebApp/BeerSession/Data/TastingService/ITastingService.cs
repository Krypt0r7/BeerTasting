using System.Threading.Tasks;
using BeerSession.Models;
using Microsoft.AspNetCore.Identity;

namespace BeerSession.Data.TastingService
{
    public interface ITastingService
    {
        Task<Tasting> CreateTastingAsync(IdentityUser user, Tasting tasting);
        Task<User> GetUserAsync(IdentityUser user);
        Task RemoveTasting(string tastingId);
    }
}