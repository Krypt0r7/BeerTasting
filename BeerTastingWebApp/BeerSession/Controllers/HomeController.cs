using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerSession.Models;
using Microsoft.AspNetCore.Identity;
using BeerSession.Data;

namespace BeerSession.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext appContext;
        public HomeController(UserManager<IdentityUser> _usermanager, ApplicationDbContext _appContext)
        {
            userManager = _usermanager;
            appContext = _appContext;
        }


        public async Task<IActionResult> Index()
        {
            var userID = await userManager.GetUserAsync(HttpContext.User);

            if (userID != null)
            {
                var oldUser = appContext.User.FirstOrDefault(o => o.UserIdentity == userID.Id);
                if (oldUser == null)
                {
                    var user = new User
                    {
                        UserName = userID.UserName,
                        Name = userID.UserName,
                        Email = userID.Email,
                        UserIdentity = userID.Id
                    };

                    appContext.Add(user);
                    await appContext.SaveChangesAsync();
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
