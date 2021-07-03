using FanFan.Models;
using FanFan.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FanFan.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ApplicationContext context1;
        private UnitofWork db;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = new UnitofWork(context);
            context1 = context;
        }

        public IActionResult Index()
        {
            var Obj = db.FanFictionPosts.GetNewFivePosts();
            ViewBag.NewFive = Obj;
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult SecuredPage()
        {

            ViewBag.UserPosts = db.FanFictionPosts.GetUserPosts(User.Claims.ElementAt(0).Value.ToString());
         
            return View();
        }
        [Authorize(Roles = "Administrator, User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
