using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Anatoli.DataAccess.Repositories
{
    public class AnatoliRepository<T> : IDisposable, IRepository<T> where T : class
    {
        public AnatoliRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");

            DbContext = dbContext;

            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }

        public virtual T GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
                DbSet.Attach(entity);

            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Deleted;
            else
                DbSet.Attach(entity);

            DbSet.Remove(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entity = GetById(id);

            if (entity == null) return; // not found; assume already deleted.

            Delete(entity);
        }

        public virtual void EntryModified(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        //CodeSmell: this method should be moved to unit of work layer.
        public virtual void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }

}
