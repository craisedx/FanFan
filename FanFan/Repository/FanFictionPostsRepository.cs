using FanFan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class FanFictionPostsRepository : Repository<FanFictionPost>, IFanFictionPostsRepository
    {
        public FanFictionPostsRepository(ApplicationContext context) : base(context)
        {

        }
       
        public  List<FanFictionPost> GetNewSixPosts()
        {
           return  Enumerable.Reverse(db.FanFictionPosts.Include(u => u.Fandom).Include(u => u.AppUser)).Take(5).ToList();
        }

        public List<FanFictionPost> GetPostByNameAndShortDisc(string name, string shortdisc)
        {
            return db.FanFictionPosts.Where(p => p.Name == name).Where(p => p.ShortDescription == shortdisc).ToList();
        }
        public  override FanFictionPost Get(int id)
        {
           return db.FanFictionPosts.Include(p => p.AppUser).Include(p => p.Fandom).Where(p => p.Id == id).First();
            
        }
        public List<FanFictionPost> GetUserPosts(string id)
        {
            return db.FanFictionPosts.Include(p => p.Fandom).Where(p => p.AppUserId == id).ToList();
        }

        public async  Task<List<FanFictionPost>> GetByFandoms(int FandomId)
        {
            return await db.FanFictionPosts.Include(p => p.Fandom).Include(p=>p.AppUser).Where(p => p.FandomId == FandomId).ToListAsync();
        }
        public override List<FanFictionPost> GetList()
        {
            return db.FanFictionPosts.Include(p=>p.AppUser).ToList();

        }
        public async Task<List<FanFictionPost>> GetWithoutFandoms(int FandomId)
        {
            return await db.FanFictionPosts.Include(p => p.Fandom).Include(p => p.AppUser).Where(p => p.FandomId != FandomId).ToListAsync();
        }
    }
}
