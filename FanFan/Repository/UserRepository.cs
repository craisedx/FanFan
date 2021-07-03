using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {


        }
        public List<AppUser> UserByName(string name)
        {
            return db.Users.Where(p => p.UserName == name).ToList();
        }
    }
}
