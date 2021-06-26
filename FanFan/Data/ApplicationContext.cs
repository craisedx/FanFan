using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Models
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        DbSet<AppUser> AppUser;
    }
}
