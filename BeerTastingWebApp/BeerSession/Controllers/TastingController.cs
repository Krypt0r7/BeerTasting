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

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddParticipants(Tasting tasting)
        {

            return View(tasting);
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
            }
            return RedirectToAction("AddParticipants", tasting);
        }
    }
}