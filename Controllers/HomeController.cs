using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using belajarnetcoremvc.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace belajarnetcoremvc.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ViewData["roles"] = JsonConvert.SerializeObject(User.Claims);
            return View();
        }
        [Authorize(Roles="Penulis")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page." +User.Identity.Name;

            return View();
        }
        [Authorize(Roles="Pemabaca")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page."+User.Identity.Name;;

            return View();
        }

              public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
