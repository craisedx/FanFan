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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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
            var Fandoms = db.Fandoms.GetList();
            ViewBag.NewFive = Obj;
            ViewBag.Fandoms = Fandoms;
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult SecuredPage()
        {
            List<int> Counts = new List<int>();
            Counts.Clear();
            var UserPosts = db.FanFictionPosts.GetUserPosts(User.Claims.ElementAt(0).Value.ToString());
            ViewBag.UserPosts = UserPosts;
            

            for (int i=0; i < UserPosts.Count; i++)
            {
            var fser = db.Chapters.AllChapterByPost(UserPosts[i].Id);
                Counts.Add(fser.Count);
            }
            ViewBag.Count = Counts;
            return View();
        }
        public IActionResult FanFiction(int id)
        {
            var FanFindById = db.FanFictionPosts.Get(id);
            var test = FanFindById.AppUser.UserName;
            ViewBag.FFBI = FanFindById;
            var GetAllChapters = db.Chapters.AllChapterByPost(FanFindById.Id);
            ViewBag.AllChap = GetAllChapters;
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
