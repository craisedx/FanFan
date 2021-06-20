using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options )
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            string AdmRoleName = "admin";
            string userRoleName = "user";
            Role adminRole = new Role { Id = 1, Name = AdmRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            AppUser AdmUser = new AppUser { Id = 1, Name="Alex", Email = "admin@fanfan.ru", Password = "1234", Condition = 1, RoleId = adminRole.Id };
           
            builder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            builder.Entity<AppUser>().HasData(new AppUser[] { AdmUser });
            
            base.OnModelCreating(builder);
            
            
            
            
            
            
            
            
            //   builder.Entity<User>().HasKey(u => u.Id);
         /*   builder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Georgiy",
                Password = "123",
                Email = "grisha@gmail.com",
                RegistrationDate = DateTime.Now,
                LastLoginDate = DateTime.Now,
                UserState = Status.Active,
                IsChecked = false

            });*/

        }
        
    }
}
