using FanFan.Models;
using FanFan.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Controllers
{
    public class FanFictionController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private UnitofWork db;
        public FanFictionController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = new UnitofWork(context);


        }
        [HttpGet]
        public IActionResult Create()
        {
            var fandoms = db.Fandoms.GetList();
            ViewBag.AllFandoms = fandoms;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostAndChapter fanfikandonechapter)
        {
            string id = User.Claims.ElementAt(0).Value.ToString();
            if (ModelState.IsValid)
            {
                FanFictionPost newFanFiction = new FanFictionPost { AppUserId = id, Name = fanfikandonechapter.Name, ShortDescription = fanfikandonechapter.ShortDescription, FandomId = fanfikandonechapter.FandomId, Picture = fanfikandonechapter.Picture };
                db.FanFictionPosts.Create(newFanFiction);
                db.Complete();
                var idPost = db.FanFictionPosts.GetPostByNameAndShortDisc(newFanFiction.Name, newFanFiction.ShortDescription);
                db.Chapters.Create(new Chapter { ChapterText = fanfikandonechapter.ChapterText, FanFictionPostId=idPost[0].Id,Name = fanfikandonechapter.ChapterName, Picture = fanfikandonechapter.ChapterPicture });
                db.Complete();
                
            }
            return RedirectToAction("SecuredPage", "Home");
        }
    }
}
