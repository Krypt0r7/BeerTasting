using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerSession.Data;
using BeerSession.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerSession.Controllers
{
    [Authorize]
    public class TastingController : Controller
    {
        private readonly ApplicationDbContext appContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TastingController(ApplicationDbContext _appContext, UserManager<IdentityUser> usermanager)
        {
            appContext = _appContext;
            _userManager = usermanager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userDb = appContext.User.Include(i => i.Tastings).FirstOrDefault(f => f.UserIdentity == user.Id);
            if (userDb == null)
            {
                var newUser = new User
                {
                    UserName = user.UserName,
                    Name = user.UserName,
                    Email = user.Email,
                    UserIdentity = user.Id
                };

                appContext.Add(newUser);
                await appContext.SaveChangesAsync();
            }
            
            if (userDb.Tastings.Count > 0)
            {
                return View();
            }

            return RedirectToAction("NewTasting");
        }

        public async Task<IActionResult> MyTasting()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var dbUser = appContext.User.Include(i => i.Tastings).ThenInclude(i => i.Tasting).First(f => f.UserIdentity == user.Id);

            return View(dbUser);
        }

        [HttpGet]
        public IActionResult NewTasting()
        {
            return View();
        }


        public IActionResult AddParticipants(Tasting tasting)
        {
            var taste = appContext.Tasting.Include(g => g.Participants).First(f => f.TastingTag == tasting.TastingTag);
            return View(taste);
        }

        public IActionResult GetTasting(string tastingId)
        {
            var tasting = appContext.Tasting.First(f => f.TastingTag.ToString() == tastingId);
            return RedirectToAction("AddParticipants", tasting);
        }

        public async Task<IActionResult> RemoveTasting(string tastingId)
        {
            var tasting = appContext.Tasting.Include(i => i.Participants).First(f => f.TastingTag.ToString() == tastingId);
            foreach (var item in tasting.Participants)
            {
                appContext.Remove(item);
            }
            appContext.Remove(tasting);
            await appContext.SaveChangesAsync();
            return RedirectToAction("MyTasting");
        }

        [HttpPost]
        public async Task<IActionResult> NewTasting(Tasting tasting)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var userDB = appContext.User.First(x => x.UserIdentity == user.Id);

                tasting.TastingTag = Guid.NewGuid();
                tasting.SessionMeister = userDB;

                var newUserTaste = new UserTasting
                {
                    Tasting = tasting,
                    User = userDB
                };
                await appContext.AddAsync(newUserTaste);
                await appContext.AddAsync(tasting);
                await appContext.SaveChangesAsync();

                Response.Cookies.Append("Tasting", tasting.TastingTag.ToString());
                return RedirectToAction("AddParticipants", tasting);
            }
            return View(tasting);
        }
    }
}