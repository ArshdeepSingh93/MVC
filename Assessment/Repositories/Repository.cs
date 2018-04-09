using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Assessment.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private EmployeeManagementContext db;
        private DbSet<T> dbSet;

        public Repository()
        {
            db = new EmployeeManagementContext();
            dbSet = db.Set<T>();
        }
        public void Delete(object Id)
        {
            T getObjById = dbSet.Find(Id);
            dbSet.Remove(getObjById);
        }

        public IEnumerable<T> GetAll()
        {

            return dbSet.ToList();
        }
       
        
        
        public T GetById(object Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }

        public void Save()
        {
          
            db.SaveChanges();
        }

       
        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        
               
        }
      
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }

    }
}