using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FanFan.Models;
using FanFan.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FanFan.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment HostEnv;
        private readonly UserManager<AppUser> userManager;
        private readonly ApplicationContext context;
        private readonly SignInManager<AppUser> signInManager;
        public readonly string CloudName = "fanfancloud";
        public readonly string apiKey = "385355166726432";
        public readonly string apiSecret = "uKsN5XyN8oWOcob4Remt_2l_T60";
        private UnitofWork db;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context, IWebHostEnvironment HostEnv)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = new UnitofWork(context);
            this.HostEnv = HostEnv;
            this.context = context;
        }
        public IActionResult Index()
        {

            return View(userManager);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.UserName, Password = model.Password, PhotoUser = "https://res.cloudinary.com/fanfancloud/image/upload/v1626040001/Profiles/profile_man_user_home_eebfno.jpg", Email = model.Email, UserState = Status.Active };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailConfim emailService = new EmailConfim();
                    await emailService.SendEmailDefault(model.Email, "Подтверждение аккаунта",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href=\"{callbackUrl}\">link</a><br> {callbackUrl}");


                    return RedirectToAction("ConfimEmail");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }
        public IActionResult UserChange(string id)
        {
            var user = db.Users.Get(id);
            ViewBag.User = user;
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult UserChange(UserChange model)
        {
           
                string UploadFolder = null;
                string uniqueFileName = null;
                string filePath = null;
                if (model.PhotoUser != null)
                {
                    UploadFolder = Path.Combine(HostEnv.WebRootPath, "Files");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PhotoUser.FileName;
                    filePath = Path.Combine(UploadFolder, uniqueFileName);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        model.PhotoUser.CopyTo(fs);

                    }
                }
                var MyAccount = new Account { ApiKey = apiKey, ApiSecret = apiSecret, Cloud = CloudName };
                Cloudinary cloudinary = new Cloudinary(MyAccount);
                string imagePath = UploadFolder + "\\" + uniqueFileName;


            AppUser newUser = db.Users.Get(model.Id);
            newUser.PhotoUser = UploadImage(imagePath, cloudinary).ToString();
              
                System.IO.File.Delete(filePath);

                db.Users.Update(newUser);
                db.Complete();
            
                return RedirectToAction("Index", "Home");
            
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
            catch (Exception)
            {
                TempData["Error"] = "Ошибка";
            }
            return ImageUrl;
        }
                [HttpGet]
                public IActionResult Login(string returnUrl = null)
                {

                    return View(new LoginModel { ReturnUrl = returnUrl });
                }
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Login(LoginModel model)
                {
                    if (ModelState.IsValid)
                    {
                        var user = userManager.FindByNameAsync(model.UserName).Result;
                        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                        if (result.Succeeded)
                        {
                            if (user.EmailConfirmed)
                            {
                                if (user.UserState == Status.Active)
                                {
                                    await signInManager.SignInAsync(user, false);
                                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                                    {
                                        return Redirect(model.ReturnUrl);
                                    }
                                    else
                                    {
                                        return RedirectToAction("Index", "Home");
                                    }
                                }
                                else
                                {
                                    TempData["UserStateNoActive"] = "Аккаут заблокирован или удален";
                                }
                            }
                            else
                            {
                                TempData["EmailError"] = "Ошибка, email не подтвержден";
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                        }
                    }
                    return View(model);
                }
                [Authorize]
                public IActionResult AccountInfo(string id)
                {
                    if (id == User.Claims.ElementAt(0).Value || User.Claims.ElementAt(2).Value == "Administrator")
                    {
                        var UserAccount = db.Users.Get(id);
                        var UserRole = db.Users.GetClaimRole(id);
                        var UserPosts = db.FanFictionPosts.GetUserPosts(id);
                        ViewBag.UserAccount = UserAccount;
                        ViewBag.UserRole = UserRole;
                        ViewBag.UserPosts = UserPosts;
                        return View();
                    }
                    return RedirectToAction("Index", "Home");
                }
                [Authorize]
                public IActionResult Edit()
                {
                    var UserAccount = db.Users.Get(User.Claims.ElementAt(0).Value);

                    ViewBag.UserAccount = UserAccount;

                    return View();
                }
                [AcceptVerbs("GET", "POST")]
                public IActionResult CheckEmail(string email)
                {
                    if (email == "admin@mail.ru")
                    {
                        return Json(false);
                    }
                    return Json(false);
                }

                [Authorize]
                public async Task<IActionResult> Logout()
                {
                    await signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
        }
