using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Core
{
    public class CoreRepository<T> : IDisposable where T : class
    {
        private IServiceProvider _serviceProvider;
        protected CoreDbContext context;
        protected DbSet<T> DbSet
        {
            get; set;
        }
        //public CoreRepository()
        //{
        //    DbSet = context.Set<T>();
        //}
        public T Add(T obj)
        {
            return DbSet.Add(obj).Entity;
        }
        public T Update(T obj)
        {
            DbSet.Add(obj);
            context.Entry(obj).State = EntityState.Modified;
            return obj;
        }
        public List<T> GetAll()
        {          
            return DbSet.ToListAsync().Result;           
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();           
        }

        public IQueryable<T> GetAllAsQuerable()
        {
            return DbSet.AsQueryable();
        }

        public T GetByID(Guid id)
        {
            return DbSet.Find(id);
        }
        public T Delete(Guid id)
        {
            T obj = DbSet.Find(id);
            if (obj == null)
                return null;
            return DbSet.Remove(obj).Entity;
        }

        public void SaveChanges()
        {
            context.SaveChanges();

        }
        public void DisableValidationAndSaveChanges()
        {          
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.SaveChanges();
            context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
        public CoreRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            context = new CoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<CoreDbContext>>());
            DbSet = context.Set<T>();
        }
        //public CoreRepository(string connection)
        //{
        //    DbSet = context.Set<T>();
        //}
        public virtual void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }

        }
    }
}

