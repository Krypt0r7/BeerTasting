using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerTastingWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerTastingWebApp.Controllers
{
    public class TastingController : Controller
    {
        private readonly AppDBContext appContext;

        public TastingController(AppDBContext _appContext)
        {
            appContext = _appContext;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddParticipants()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewTasting(Tasting tasting)
        {
            if (ModelState.IsValid)
            {
                var cookiestring = Request.Cookies["LoggedIn"];

                var loggedInUser = appContext.User.Where(u => u.UserTag.ToString() == cookiestring).FirstOrDefault();

                tasting.TastingTag = Guid.NewGuid();

                tasting.SessionMeister = loggedInUser;

                appContext.Add(tasting);
                appContext.SaveChanges();

                Response.Cookies.Append("Tasting", tasting.TastingTag.ToString());
            }
            return RedirectToAction("AddParticipant");
        }
    }
}