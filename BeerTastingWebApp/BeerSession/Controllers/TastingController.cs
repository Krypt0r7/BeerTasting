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
using Microsoft.AspNetCore.SignalR;
using BeerSession.Hubs;
using BeerSession.Data.TastingService;

namespace BeerSession.Controllers
{
    [Authorize]
    public class TastingController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITastingService tastingService;

        public TastingController(
            ApplicationDbContext _appContext, 
            UserManager<IdentityUser> _usermanager, 
            ITastingService _tastingService)
        {
            dbContext = _appContext;
            userManager = _usermanager;
            tastingService = _tastingService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var userDb = await tastingService.GetUserAsync(user);
            
            if (userDb.Tastings.Count > 0)
            {
                return View();
            }

            return RedirectToAction("NewTasting");
        }


        public async Task<IActionResult> MyTasting()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var dbUser = dbContext.User.Include(i => i.Tastings).ThenInclude(i => i.Tasting).First(f => f.UserIdentity == user.Id);

            return View(dbUser);
        }

        [HttpGet]
        public IActionResult NewTasting()
        {
            return View();
        }

        public IActionResult AddBeers(string tastingId)
        {
            var tasting = dbContext.Tasting.Include(v => v.Beers).First(x => x.TastingTag.ToString() == tastingId);

            return View(tasting);
        }

        public IActionResult AddParticipants(Tasting tasting)
        {
            var taste = dbContext.Tasting.Include(g => g.Participants).First(f => f.TastingTag == tasting.TastingTag);
            return View(taste);
        }

        public IActionResult GetTasting(string tastingId)
        {
            var tasting = dbContext.Tasting.First(f => f.TastingTag.ToString() == tastingId);
            return RedirectToAction("AddParticipants", tasting);
        }

        public async Task<IActionResult> RemoveTasting(string tastingId)
        {
            await tastingService.RemoveTasting(tastingId);
            return RedirectToAction("MyTasting");
        }

        public async Task<IActionResult> Session(Guid tastingId)
        {
            var tasting = dbContext.Tasting.Where(t => t.TastingTag == tastingId).Include(i => i.Participants).Include(i => i.Beers).First();
            return View(tasting);
        }

        public async Task<IActionResult> CatchParticipant(string tastingId)
        {
            var tasting = dbContext.Tasting.FirstOrDefault(f => f.TastingTag.ToString() == tastingId);

            return RedirectToAction("Session", tasting);
        }

        [HttpPost]
        public async Task<IActionResult> NewTasting(Tasting tasting)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                var tastingDb = await tastingService.CreateTastingAsync(user, tasting);

                return RedirectToAction("AddParticipants", tastingDb);
            }
            return View(tasting);
        }
    }
}