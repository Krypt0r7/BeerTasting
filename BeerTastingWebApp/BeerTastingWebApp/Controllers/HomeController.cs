﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerTastingWebApp.Models;

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
        [Route("signup")]
        public IActionResult SignupVal(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            return Redirect("tasting");
        }
    }
}
