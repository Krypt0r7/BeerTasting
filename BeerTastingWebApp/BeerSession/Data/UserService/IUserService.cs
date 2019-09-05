using System.Threading.Tasks;
using BeerSession.Models;
using Microsoft.AspNetCore.Identity;

namespace BeerSession.Data.UserService
{
    public interface IUserService
    {
        Task CreateUserAsync(IdentityUser user);
    }
}