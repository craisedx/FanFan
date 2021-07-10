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
using System.Threading.Tasks;
using System.Web;

namespace FanFan.Controllers
{
    public class FanFictionController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private UnitofWork db;
        private IWebHostEnvironment HostEnv;
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
        [HttpGet]
        public IActionResult Create()
        {
            var fandoms = db.Fandoms.GetList();
            ViewBag.AllFandoms = fandoms;
            return View();
        }
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var files = Request.Form.Files.First();
            var fileDic = "Files";
            string filePath = Path.Combine(HostEnv.WebRootPath, fileDic);
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var fileName = file.FileName;
            filePath = Path.Combine(filePath, fileName);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);

            }
            var MyAccount = new Account { ApiKey = apiKey, ApiSecret = apiSecret, Cloud = CloudName };
            Cloudinary cloudinary = new Cloudinary(MyAccount);
            string imagePath = filePath;
            UploadImage(imagePath, cloudinary);
            return RedirectToAction("Create");
        }
        public void UploadImage(string imagepath, Cloudinary cloudinary)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagepath)
                };
                var uploadresult = cloudinary.Upload(uploadParams);
                

            }
            catch(Exception)
            {
                TempData["EmailError"] = "Ошибка, email не подтвержден";
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FanFictionPostViewModel fanfikandonechapter)
        {

            
            if (ModelState.IsValid)
            {
                FanFictionPost newFanFiction = new FanFictionPost { AppUserId = fanfikandonechapter.AppUserId, Name = fanfikandonechapter.Name, ShortDescription = fanfikandonechapter.ShortDescription, FandomId = fanfikandonechapter.FandomId};
                db.FanFictionPosts.Create(newFanFiction);
                db.Complete();
               
                
            }
            return RedirectToAction("SecuredPage", "Home");
        }
    }
}
