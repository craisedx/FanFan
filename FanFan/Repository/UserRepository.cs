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
        public override AppUser Get(string id)
        {
            return db.Users.Where(p => p.Id == id).FirstOrDefault();

        }
        public string GetClaimRole(string id)
        {
            return db.UserClaims.Where(x => x.UserId == id).Select(x => x.ClaimValue).FirstOrDefault();

        }
        public void UserCommandInit(List<AppUser> users, string action)
        {

            if (action == "sub1")
                BlockUser(users);
            else if (action == "sub2")
                UnblockUser(users);
            else if (action == "sub3")
                DeleteUser(users);

        }
        
        private void DeleteUser(List<AppUser> users)
        {
            var test = db.Users.ToList();
            for (int i = 0; i < test.Count; i++)
            {
                test[i].IsChecked = users[i].IsChecked;
            }
            db.Users.UpdateRange(test);
            db.SaveChanges();

            IEnumerable<AppUser> user1 = db.Users
                .Where(c => c.IsChecked == true)
                .AsEnumerable().Select(c => {
                    c.UserState = Status.Delete;
                    return c;
                }
                );

            ModEntry(user1);
            db.SaveChanges();
        }

        public void UnblockUser(List<AppUser> users)
        {
            var test = db.Users.ToList();
            for (int i = 0; i < test.Count; i++)
            {
                test[i].IsChecked = users[i].IsChecked;
            }
            db.Users.UpdateRange(test);
            db.SaveChanges();

            IEnumerable<AppUser> user1 = db.Users
                .Where(c => c.IsChecked == true && c.UserState == Status.Block)
                .AsEnumerable().Select(c => {
                    c.UserState = Status.Active;
                    return c;
                }
                );

            ModEntry(user1);
            db.SaveChanges();
        }

        public void BlockUser(List<AppUser> users)
        {
            var test = db.Users.ToList();
            for(int i =0; i<test.Count; i++)
            {
                test[i].IsChecked = users[i].IsChecked;
            }
            db.Users.UpdateRange(test);
            db.SaveChanges();

            IEnumerable<AppUser> user1 = db.Users
                .Where(c => c.IsChecked == true && c.UserState == Status.Active)
                .AsEnumerable().Select(c => {
                    c.UserState = Status.Block;
                    return c;
                }
                );

            ModEntry(user1);
            db.SaveChanges();

        }

        public void RemoveMark()
        {
            IEnumerable<AppUser> users = db.Users

        .Where(c => c.IsChecked == true)
        .AsEnumerable()

        .Select(c => {

            c.IsChecked = false;
            return c;
        });
            ModEntry(users);
            db.SaveChanges();
        }
    }
}
