using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public interface IFandomRepository : IRepository<Fandom>
    {
        IEnumerable<Fandom> GetTopFandoms(int id); 
    }
}
