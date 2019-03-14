using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerTastingWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeerTastingWebApp.Controllers
{
    public class TastingController : Controller
    {
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

            }
            return View();
        }
    }
}