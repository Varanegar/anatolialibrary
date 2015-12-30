using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Anatoli.DataAccess.Repositories
{
    public abstract class AnatoliRepository<T> : IDisposable, IRepository<T> where T : class
    {
        #region Properties
        public AnatoliDbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        #endregion

        #region Ctors
        public AnatoliRepository(AnatoliDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");

            DbContext = dbContext;

            DbSet = DbContext.Set<T>();

            DbContext.Database.Log = Console.Write;
        }
        #endregion

        #region Methods
        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            throw new InvalidOperationException();
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            var model = await DbSet.SingleOrDefaultAsync(match);

            return model;
        }
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await DbSet.Where(match).ToListAsync();
        }

        public virtual void Add(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            var dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);

            var factory = Task<T>.Factory;

            return await factory.StartNew(() => entity);
        }
        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
                DbSet.Attach(entity);

            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual async Task<T> UpdateAsync(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
                DbSet.Attach(entity);

            dbEntityEntry.State = EntityState.Modified;

            var factory = Task<T>.Factory;

            return await factory.StartNew(() => entity);
        }
        public virtual async Task DeleteAsync(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Deleted;
            else
                DbSet.Attach(entity);

            var factory = Task.Factory;

            await factory.StartNew(() => DbSet.Remove(entity));
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = GetByIdAsync(id);

            if (entity == null)
                return; // not found; assume already deleted.

            await DeleteAsync(entity.Result);
        }
        public virtual void EntryModified(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
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
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
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
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }
        #endregion

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
