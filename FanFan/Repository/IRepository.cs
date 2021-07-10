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
        TEntity Get(string id); 
        void Create(TEntity item);
        void CreateRange(List<TEntity> items);
        void UpdateRange(List<TEntity> entities);
        void ModEntry(IEnumerable<TEntity> entitys);
        void Delete(int id);
        void Save();  
    }
}
