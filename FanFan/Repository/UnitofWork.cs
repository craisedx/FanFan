using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationContext context;
        public UnitofWork(ApplicationContext context)
        {
            this.context = context;
            Fandoms = new FandomRepository(context);
            FanFictionPosts = new FanFictionPostsRepository(context);
            Chapters = new ChapterRepository(context);
            Users = new UserRepository(context);
            Comments = new CommentRepository(context);
        }
        public IFandomRepository Fandoms { get; private set; }
        public IFanFictionPostsRepository FanFictionPosts { get; private set; }
        public IChapterRepository Chapters { get; private set; }
        public IUserRepository Users { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
