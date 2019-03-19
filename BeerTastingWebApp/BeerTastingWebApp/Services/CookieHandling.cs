using Microsoft.AspNetCore.Mvc;
using BeerTastingWebApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeerTastingWebApp.Services
{
    public class CookieHandling
    {
        private readonly AppDBContext _dBContext;

        public CookieHandling(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public User IsUserLoggedIn(string cookieString)
        {
            var user = _dBContext.User.Where(u => u.UserTag.ToString() == cookieString).FirstOrDefault();
            return user;
        }
    }
}
