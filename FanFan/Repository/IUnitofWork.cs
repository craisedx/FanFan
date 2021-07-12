using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
   public interface IUnitofWork : IDisposable
    {
        IFandomRepository Fandoms { get; }
        IFanFictionPostsRepository FanFictionPosts { get;}
        IChapterRepository Chapters { get;}
        ICommentRepository Comments { get;}
        int Complete();
    }
}
