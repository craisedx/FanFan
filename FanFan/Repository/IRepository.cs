using FanFan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetList();
        TEntity Get(int id); 
        void Create(TEntity item);
  
        void Delete(int id);
        void Save();  
    }
}
