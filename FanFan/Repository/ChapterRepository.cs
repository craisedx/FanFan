using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        public ChapterRepository(ApplicationContext context) : base(context)
        {

        }

        public List<Chapter> AllChapterByPost(int PostId)
        {
            return db.Chapters.Where(p => p.FanFictionPostId == PostId).ToList();
        }

        public int AllChapterCountByPost(int PostId)
        {
            
            return db.Chapters.Where(p => p.FanFictionPostId == PostId).ToList().Count();
        }
    }
}
