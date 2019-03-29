using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerSession.Models;
using Microsoft.AspNetCore.Identity;

namespace BeerSession.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        public HomeController(UserManager<IdentityUser> usermanager)
        {
            _userManager = usermanager;
        }


        public async Task<IActionResult> Index()
        {
            var userID = await _userManager.GetUserAsync(HttpContext.User);
            if (userID != null)
            {
                var user = new User
                {
                    Name = userID.UserName,
                    Email = userID.Email,
                    UserIdentity = userID.Id
                };
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
