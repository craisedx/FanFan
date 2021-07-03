using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class FandomRepository : Repository<Fandom>, IFandomRepository
    {
        public FandomRepository(ApplicationContext context) : base(context)
        {
            
        }
        public IEnumerable<Fandom> GetTopFandoms(int f)
        {
            return db.Fandoms.Take(5).ToList();
        }
       
    }
}
