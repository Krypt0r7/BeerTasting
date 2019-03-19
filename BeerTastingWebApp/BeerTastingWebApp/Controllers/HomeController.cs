using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerTastingWebApp.Models;
using Microsoft.AspNetCore.Http;
using BeerTastingWebApp.Services;

namespace BeerTastingWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _dbContext;

        public HomeController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }


        public IActionResult Index()
        {
            //var user = CookieHandling.IsUserLoggedIn(HttpContext.Session.GetString("LoggedIn");
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUser(User user)
        {
            var userDb = _dbContext.User.Where(u => u.Email == user.Email).FirstOrDefault();
            if (user != null)
            {
                if (userDb.Password == user.Password)
                {
                    user.LoggedIn = true;
                    _dbContext.SaveChanges();
                    Response.Cookies.Append("LoggedIn", userDb.UserTag.ToString());
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignupVal(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserTag = Guid.NewGuid();
                _dbContext.Add(user);
                _dbContext.SaveChanges();

                Response.Cookies.Append("LoggedIn", user.UserTag.ToString());

                return Redirect("/Index");
            }

            return View(user);
        }
    }
}
