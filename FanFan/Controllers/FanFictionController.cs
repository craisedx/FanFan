using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FanFan.Models;
using FanFan.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FanFan.Controllers
{
    public class FanFictionController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private UnitofWork db;
        private readonly IWebHostEnvironment HostEnv;
        public readonly string CloudName = "fanfancloud";
        public readonly string apiKey = "385355166726432";
        public readonly string apiSecret = "uKsN5XyN8oWOcob4Remt_2l_T60";
        public FanFictionController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context, IWebHostEnvironment HostEnv)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = new UnitofWork(context);
            this.HostEnv = HostEnv;
           

        }
        public IActionResult EditChapter(int id)
        {
            var markdown = new MarkdownSharp.Markdown();
            var Chapter = db.Chapters.Get(id);
            Chapter.ChapterText = markdown.Transform(Chapter.ChapterText);
            ViewBag.Chapter = Chapter;
            return View();
        }
        [HttpPost]
        public IActionResult EditChapter(Chapter model)
        {
            Chapter chapter = db.Chapters.Get(model.Id);
            if (model.ChapterText != null)
                chapter.ChapterText = model.ChapterText;
            if (model.Name != null)
                chapter.Name = model.Name;
            db.Chapters.Update(chapter);
            db.Complete();
            return Redirect($"/Home/FanFiction/{chapter.FanFictionPostId}");
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            var fandoms = db.Fandoms.GetList();
            ViewBag.AllFandoms = fandoms;
            return View();
        }
        public Uri UploadImage(string imagepath, Cloudinary cloudinary)
        {
            Uri ImageUrl = null;
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagepath)
                };
                var uploadresult = cloudinary.Upload(uploadParams);

                ImageUrl = uploadresult.SecureUrl;
            }
            catch(Exception)
            {
                TempData["Error"] = "Ошибка";
            }
            return ImageUrl;
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(FanFictionPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UploadFolder = null;
                string uniqueFileName = null;
                string filePath = null;
                if (model.Picture != null)
                {
                   UploadFolder =  Path.Combine(HostEnv.WebRootPath, "Files");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    filePath = Path.Combine(UploadFolder, uniqueFileName);
                    
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        model.Picture.CopyTo(fs);

                    }
                var MyAccount = new Account { ApiKey = apiKey, ApiSecret = apiSecret, Cloud = CloudName };
                Cloudinary cloudinary = new Cloudinary(MyAccount);
                string imagePath = UploadFolder +"\\"+uniqueFileName;
                
                
                FanFictionPost newFanFiction = new FanFictionPost
                {
                    AppUserId = model.AppUserId,
                    Name = model.Name,
                    ShortDescription = model.ShortDescription,
                    FandomId = model.FandomId,
                    Picture = UploadImage(imagePath, cloudinary).ToString()
                };
                    System.IO.File.Delete(filePath);


                    db.FanFictionPosts.Create(newFanFiction);
                    db.Complete();
                }
                else
                {
                    FanFictionPost newFanFiction = new FanFictionPost
                    {
                        AppUserId = model.AppUserId,
                        Name = model.Name,
                        ShortDescription = model.ShortDescription,
                        FandomId = model.FandomId,
                       
                    };
                    System.IO.File.Delete(filePath);


                    db.FanFictionPosts.Create(newFanFiction);
                    db.Complete();

                }

            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult DeletePost(int id)
        {
             db.FanFictionPosts.Delete(id);
            db.Complete();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteChapter(int id)
        {
            db.Chapters.Delete(id);
            db.Complete();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult EditPost(int id)
        {
            ViewBag.Post = db.FanFictionPosts.Get(id);
            var fandoms = db.Fandoms.GetList();
            ViewBag.AllFandoms = fandoms;
            return View();
        }
        [HttpPost]
        public IActionResult EditPost(FanFictionPost model)
        {
            var FanPost = db.FanFictionPosts.Get(model.Id);
            if(model.Name != null)
            {
                FanPost.Name = model.Name;
            }
            if (model.ShortDescription != null)
                FanPost.ShortDescription = model.ShortDescription;
            FanPost.FandomId = model.FandomId;
            db.FanFictionPosts.Update(FanPost);
            db.Complete();
            return Redirect($"/Home/FanFiction/{FanPost.Id}");
        }
        public IActionResult AddChapter(int id )
        {
            ViewBag.PostId = db.FanFictionPosts.Get(id);
            return View();
        }
        [HttpPost]
        public IActionResult AddChapter(ChapterModel chapter)
        {
            if (ModelState.IsValid)
            {
                string UploadFolder = null;
                string uniqueFileName = null;
                string filePath = null;
                if (chapter.Picture != null)
                {
                    UploadFolder = Path.Combine(HostEnv.WebRootPath, "Files");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + chapter.Picture.FileName;
                    filePath = Path.Combine(UploadFolder, uniqueFileName);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        chapter.Picture.CopyTo(fs);

                    }
                }
                var MyAccount = new Account { ApiKey = apiKey, ApiSecret = apiSecret, Cloud = CloudName };
                Cloudinary cloudinary = new Cloudinary(MyAccount);
                string imagePath = UploadFolder + "\\" + uniqueFileName;
                var markdown = new MarkdownSharp.Markdown();
                Chapter newChapter = new Chapter
                {
                    Name = chapter.Name,
                    ChapterText = chapter.ChapterText,
                    Picture = UploadImage(imagePath, cloudinary).ToString(),
                    FanFictionPostId=chapter.FanFictionPostId
                };

                System.IO.File.Delete(filePath);


                db.Chapters.Create(newChapter);
                db.Complete();
            }
            return Redirect($"/Home/FanFiction/{chapter.Id}");
        }
    }
}
