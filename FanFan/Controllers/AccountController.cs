using FanFan.Models;
using FanFan.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FanFan.Controllers
{
    public class AccountController : Controller
    {
      
        private readonly UserManager<AppUser> userManager;
        private readonly ApplicationContext context;
        private readonly SignInManager<AppUser>  signInManager;
        
        private UnitofWork db;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            db = new UnitofWork(context);
          
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
                AppUser user = new AppUser { UserName = model.UserName, Password = model.Password, Email = model.Email, UserState=Status.Active };
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
        public IActionResult ConfimEmail()
        {
            return View();
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
            if (ModelState.IsValid) {
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
            var UserAccount = db.Users.Get(id);
            var UserRole = db.Users.GetClaimRole(id);
            ViewBag.UserAccount = UserAccount;
            ViewBag.UserRole = UserRole;
            return View();
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
            if(email == "admin@mail.ru")
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
