using FanFan.Models;
using FanFan.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private UnitofWork db;
        private readonly ApplicationContext context1;
        public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = new UnitofWork(context);
            context1 = context;


        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Panel()
        {
            var model =  db.Users.GetList();
            var allfan = db.FanFictionPosts.GetList();
            List<int> Counts = new List<int>();
            Counts.Clear();
            for (int i = 0; i < allfan.Count; i++)
            {
                var fser = db.Chapters.AllChapterByPost(allfan[i].Id);
                Counts.Add(fser.Count);
            }
            ViewBag.Count = Counts;
            ViewBag.AllFan = allfan;
            return View(model);
        }
        [HttpPost]
        public  IActionResult Panel(List<AppUser> users, IFormCollection form)
        {
            string str = form.Keys.FirstOrDefault();
            
            db.Users.UserCommandInit(users, str);
            db.Users.RemoveMark();
            return RedirectToAction("panel");
        }
    }
}
