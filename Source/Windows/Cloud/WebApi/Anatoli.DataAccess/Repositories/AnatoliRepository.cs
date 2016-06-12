using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using EntityFramework.Caching;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using LinqKit;
using AutoMapper.QueryableExtensions;
using Anatoli.DataAccess.Models;

namespace Anatoli.DataAccess.Repositories
{
    public abstract class BaseAnatoliRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        #region Properties
        public OwnerInfo OwnerInfo { get; set; }

        public AnatoliDbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public Expression<Func<T, bool>> ExtraPredicate { get; set; }
        #endregion

        #region Ctors
        public BaseAnatoliRepository(AnatoliDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");

            DbContext = dbContext;

            DbSet = DbContext.Set<T>();

            DbContext.Database.Log = Console.Write;
        }
        public BaseAnatoliRepository(AnatoliDbContext dbContext, OwnerInfo ownerInfo) : this(dbContext)
        {
            OwnerInfo = ownerInfo;
        }
        #endregion

        #region Methods
        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }

        public virtual T GetById(Guid id)
        {
            return DbSet.Find(id);
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
       
        public virtual List<T> GetAll()
        {
            return DbSet.AsExpandable()
                        .Where(CalcExtraPredict())
                        .AsNoTracking()
                        .ToList();
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await DbSet.AsExpandable()
                              .Where(CalcExtraPredict())
                              .AsNoTracking()
                              .ToListAsync();
        }
        public virtual async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            var query = GetQuery().AsExpandable().Where(CalcExtraPredict()).AsNoTracking();

            if (selector != null)
                return await query.Select(selector).ToListAsync();

            return await query.ProjectTo<TResult>().ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await DbSet.AsExpandable().SingleOrDefaultAsync(CalcExtraPredict(predicate));
            }catch(Exception ex)
            {
                return null;
            }
        }
        public virtual async Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            if (selector != null)
                return await DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).Select(selector).SingleOrDefaultAsync();

            return await DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).ProjectTo<TResult>().SingleOrDefaultAsync();
        }

        public virtual async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).ToListAsync();
        }
        public virtual async Task<List<TResult>> FindAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            if (selector != null)
                return await DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).Select(selector).ToListAsync();

            return await DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).ProjectTo<TResult>().ToListAsync();
        }

        public virtual IEnumerable<T> GetFromCached(Expression<Func<T, bool>> predicate, int cacheTimeOut = 300)
        {
            return DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300)));
        }
        public virtual async Task<IEnumerable<T>> GetFromCachedAsync(Expression<Func<T, bool>> predicate, int cacheTimeOut = 300)
        {
            return await DbSet.AsExpandable().Where(CalcExtraPredict(predicate)).FromCacheAsync(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300)), tags: new List<string> { typeof(T).ToString() });
        }
        public virtual async Task<IEnumerable<TResult>> GetFromCachedAsync<TResult>(Expression<Func<T, bool>> predicate,
                                                                                    Expression<Func<T, TResult>> selector,
                                                                                    int cacheTimeOut = 300) where TResult : class
        {
            if (selector != null)
                return await DbSet.AsExpandable()
                                  .Where(CalcExtraPredict(predicate))
                                  .Select(selector)
                                  .FromCacheAsync(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300)), tags: new List<string> { typeof(T).ToString() });

            return await DbSet.AsExpandable()
                              .Where(CalcExtraPredict(predicate))
                              .ProjectTo<TResult>()
                              .FromCacheAsync(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(300)), tags: new List<string> { typeof(T).ToString() });
        }

        protected virtual Expression<Func<T, bool>> CalcExtraPredict(Expression<Func<T, bool>> predict = null)
        {
            if (predict == null)
                predict = p => true;

            if (ExtraPredicate != null)
                predict = PredicateBuilder.And(predict, ExtraPredicate);

            return predict;
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
        public virtual void UpdateBatch(Expression<Func<T, bool>> predicate, T entity)
        {
            DbSet.Where(predicate).Update(t => entity);
        }
        public virtual async Task UpdateBatchAsync(Expression<Func<T, bool>> predicate, T entity)
        {
            await DbSet.Where(predicate).UpdateAsync(t => entity);
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
        public virtual async Task DeleteAsync(T entity)
        {
            await Task.Factory.StartNew(() => Delete(entity));
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = GetByIdAsync(id);

            if (entity == null)
                return; // not found; assume already deleted.

            await DeleteAsync(entity.Result);
        }
        public virtual void DeleteRange(List<T> entities)
        {
            entities.ForEach(entity =>
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                    dbEntityEntry.State = EntityState.Deleted;
                else
                    DbSet.Attach(entity);
            });

            DbSet.RemoveRange(entities);
        }
        public virtual async Task DeleteRangeAsync(List<T> entities)
        {
            await Task.Factory.StartNew(() => DeleteRange(entities));
        }
        public virtual void DeleteBatch(Expression<Func<T, bool>> predicate)
        {
            DbSet.Where(predicate).Delete();
        }
        public virtual async Task DeleteBatchAsync(Expression<Func<T, bool>> predicate)
        {
            await DbSet.Where(predicate).DeleteAsync();
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

                ExpireCache();
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

                ExpireCache();
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

        public virtual int Count()
        {
            return DbSet.Count();
        }
        public virtual async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        private void ExpireCache()
        {
            var cacheKey = typeof(T).ToString();

            CacheManager.Current.Expire(cacheKey);
        }
        #endregion

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }

    public abstract class AnatoliRepository<T> : BaseAnatoliRepository<T>, IDisposable, IRepository<T> where T : BaseModel, new()
    {
        #region Ctors
        public AnatoliRepository(AnatoliDbContext dbContext) : base(dbContext) { }

        public AnatoliRepository(AnatoliDbContext dbContext, OwnerInfo ownerInfo) : this(dbContext)
        {
            OwnerInfo = ownerInfo;
        }
        #endregion

        #region Methods
        public TResult GetById<TResult>(Guid id)
        {
           return DbSet.Where(p => p.Id == id).ProjectTo<TResult>().FirstOrDefault(); 
        }
        public async Task<TResult> GetByIdAsync<TResult>(Guid id)
        {
           return await DbSet.Where(p => p.Id == id).ProjectTo<TResult>().FirstOrDefaultAsync();
        }
        public async Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<T, TResult>> selector)
        {
            return await DbSet.Where(p => p.Id == id).Select(selector).FirstOrDefaultAsync();
        }

        protected override Expression<Func<T, bool>> CalcExtraPredict(Expression<Func<T, bool>> predict = null)
        {
            if (predict == null)
                predict = p => true;

            if (OwnerInfo != null)
                predict = PredicateBuilder.And(predict, p => p.ApplicationOwnerId == OwnerInfo.ApplicationOwnerKey &&
                                                             p.DataOwnerId == OwnerInfo.DataOwnerKey &&
                                                             p.IsRemoved == (OwnerInfo.RemovedData ? p.IsRemoved : false));

            if (ExtraPredicate != null)
                predict = PredicateBuilder.And(predict, ExtraPredicate);

            return predict;
        }

        #endregion
    }
}
