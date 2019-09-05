using BeerSession.Data;
using BeerSession.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.Data.UserService
{
    class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task CreateUserAsync(IdentityUser user)
        {
            var newUser = new User(){
                Email = user.Email,
                Name = user.Email,
                UserName = user.Email,
                UserIdentity = user.Id
            };

            await dbContext.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
        }
    }
}
