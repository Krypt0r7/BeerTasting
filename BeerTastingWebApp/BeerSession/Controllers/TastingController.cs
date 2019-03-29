using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerSession.Data;
using BeerSession.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeerSession.Controllers
{
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


        public async Task<IActionResult> AddParticipants()
        {
            //var user = await _userManager.GetUserAsync(HttpContext);

            return View();
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

                appContext.Add(tasting);
                appContext.SaveChanges();

                Response.Cookies.Append("Tasting", tasting.TastingTag.ToString());
            }
            return RedirectToAction("AddParticipants", tasting);
        }
    }
}