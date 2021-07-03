using FanFan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFan.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        protected readonly ApplicationContext db;
        public Repository(ApplicationContext context)
        {
            this.db = context;
        }
        public List<TEntity> GetList()
        {
            return db.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Create(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entity = db.Set<TEntity>().Find(id);
            if (entity != null)
                db.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
