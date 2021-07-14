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
        public async Task<IActionResult> AllPosts(int id)
        {
            var Fandoms = db.Fandoms.GetList();
           
            ViewBag.Fandoms = Fandoms;

            if (id != 0)
            {
                var SortedPostByFandom = await db.FanFictionPosts.GetByFandoms(id);
                SortedPostByFandom.Reverse();
                List<FanFictionPost> ToPart = await db.FanFictionPosts.GetWithoutFandoms(id);

                SortedPostByFandom.AddRange(ToPart);
                ViewBag.SortedPosts = SortedPostByFandom;
            }
            else
            {
                ViewBag.SortedPosts = Enumerable.Reverse(db.FanFictionPosts.GetList());
            }
            return View();
        }
        
        public IActionResult Index()
        {
            var Obj = db.FanFictionPosts.GetNewSixPosts();
            var Fandoms = db.Fandoms.GetList();
            ViewBag.NewFive = Obj;
            ViewBag.Fandoms = Fandoms;
            return View();
        }
        
        [Authorize]
        public IActionResult AddComment(CommentViewModel comment)
        {
            comment.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                Comment model  = new Comment { AppUserId = comment.AppUserId, Date = comment.Date, FanFictionPostId = comment.FanFictionPostId, Text = comment.Text };
                db.Comments.Create(model) ;
                db.Complete();
            }
            return Redirect($"/Home/FanFiction/{comment.FanFictionPostId}");
        }
        public IActionResult FanFiction(int id)
        {
            var markdown = new MarkdownSharp.Markdown();
            var FanFindById = db.FanFictionPosts.Get(id);
            
            ViewBag.FFBI = FanFindById;
            var GetAllChapters = db.Chapters.AllChapterByPost(FanFindById.Id);
            foreach(var item in GetAllChapters)
            {
                item.ChapterText = markdown.Transform(item.ChapterText);
            } 
            ViewBag.AllChap = GetAllChapters;
            var GetAllComments = db.Comments.GetCommentsFromPostById(id);
            
            ViewBag.AllComments = Enumerable.Reverse(GetAllComments).ToList();
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
