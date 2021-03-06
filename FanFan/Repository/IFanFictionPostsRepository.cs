using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public interface IFanFictionPostsRepository : IRepository<FanFictionPost>
    {
        List<FanFictionPost> GetNewSixPosts();
        List<FanFictionPost> GetUserPosts(string id);
        List<FanFictionPost> GetPostByNameAndShortDisc(string name, string shortdisc);
        Task<List<FanFictionPost>> GetByFandoms(int FandomId);
        Task<List<FanFictionPost>> GetWithoutFandoms(int FandomId);

    }
}
