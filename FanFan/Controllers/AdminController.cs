using FanFan.Models;
using FanFan.Repository;
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
        public IActionResult Panel()
        {
            var model =  db.Users.GetList();
            var allfan = db.FanFictionPosts.GetList();
            ViewBag.AllFan = allfan;
            return View(model);
        }
        [HttpPost]
        public  IActionResult Panel(List<AppUser> users, IFormCollection form)
        {
            string str = form.Keys.FirstOrDefault();
            
            db.Users.UserCommandInit(users, str);
            return RedirectToAction("panel");
        }
       

       
        /* public void UnblockUser(List<User> users)
         {
             context.Users.UpdateRange(users);
             context.SaveChanges();


             IEnumerable<User> user1 = context.Users

         .Where(c => c.IsChecked == true && c.UserState == Status.Block)
         .AsEnumerable()

         .Select(c => {
             c.UserState = Status.Active;

             return c;
         });

             foreach (User user in user1)
             {

                 context.Entry(user).State = EntityState.Modified;
             }

             context.SaveChanges();

         } */
       
     /*   public void DeleteUser(List<User> users)
        {
            context.Users.UpdateRange(users);
            context.SaveChanges();


            IEnumerable<User> user1 = context.Users

        .Where(c => c.IsChecked == true)
        .AsEnumerable()

        .Select(c => {
            c.UserState = Status.Delete;

            return c;
        });


            foreach (User user in user1)
            {
                // Указать, что запись изменилась
                context.Entry(user).State = EntityState.Modified;
            }
            // Сохранить изменения
            context.SaveChanges();

        }*/
    }
}
