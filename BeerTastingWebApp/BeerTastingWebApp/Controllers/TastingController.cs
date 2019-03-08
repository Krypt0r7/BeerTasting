using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BeerTastingWebApp.Controllers
{
    public class TastingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}