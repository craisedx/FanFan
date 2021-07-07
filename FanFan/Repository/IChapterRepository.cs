using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        List<Chapter> AllChapterByPost(int PostId);
        int AllChapterCountByPost(int PostId);
    }
}
