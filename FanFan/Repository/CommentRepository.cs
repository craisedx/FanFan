using FanFan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {

        }

        public List<Comment> GetCommentsFromPostById(int id)
        {
            return  db.Comments.Include(p => p.AppUser).Include(p => p.FanFictionPost).Where(p => p.FanFictionPostId == id).ToList();
        }
    }
}
