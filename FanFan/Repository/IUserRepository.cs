using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public interface IUserRepository : IRepository<AppUser>
    {
        List<AppUser> UserByName(string name);
        string GetClaimRole(string id);
        void UserCommandInit(List<AppUser> users, string action);
        void RemoveMark();
        void BlockUser(List<AppUser> users);
    }
}
