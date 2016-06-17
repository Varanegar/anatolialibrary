﻿//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Linq.Expressions;
//using System.Collections.Generic;

namespace Anatoli.DataAccess.Interfaces
{
    //public interface IBaseRepository<T> where T : class
    //{
    //    Expression<Func<T, bool>> ExtraPredicate { get; set; }
    //    AnatoliDbContext DbContext { get; set; }


    //    IQueryable<T> GetQuery();

    //    T GetById(Guid id);
    //    Task<T> GetByIdAsync(Guid id);      

    //    List<T> GetAll();
    //    Task<List<T>> GetAllAsync();
    //    Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector);
    //    Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    //    Task<TResult> FindAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
    //    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    //    Task<List<TResult>> FindAllAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);

    //    /// <summary>
    //    /// To query and get data and cache it.
    //    /// </summary>
    //    /// <param name="predicate">conditions to query data</param>
    //    /// <param name="cacheTimeOut">default timeout equals 300 seconds</param>
    //    /// <returns></returns>
    //    IEnumerable<T> GetFromCached(Expression<Func<T, bool>> predicate, int cacheTimeOut = 300);
    //    /// <summary>
    //    /// To query and get data async and cache it.
    //    /// </summary>
    //    /// <param name="predicate">conditions to query data</param>
    //    /// <param name="cacheTimeOut">default timeout equals 300 seconds</param>
    //    /// <returns></returns>
    //    Task<IEnumerable<T>> GetFromCachedAsync(Expression<Func<T, bool>> predicate, int cacheTimeOut = 300);
    //    Task<IEnumerable<TResult>> GetFromCachedAsync<TResult>(Expression<Func<T, bool>> predicate,
    //                                                           Expression<Func<T, TResult>> selector,
    //                                                           int cacheTimeOut = 300) where TResult : class;

    //    void Add(T entity);
    //    Task<T> AddAsync(T entity);

    //    void Update(T entity);
    //    Task<T> UpdateAsync(T entity);
    //    /// <summary>
    //    /// To update records without fetching.
    //    /// </summary>
    //    /// <param name="predicate">conditions to query data</param>
    //    /// <param name="entity">entity should be a new class</param>
    //    void UpdateBatch(Expression<Func<T, bool>> predicate, T entity);
    //    /// <summary>
    //    /// To update-async records without fetching.
    //    /// </summary>
    //    /// <param name="predicate">conditions to query data</param>
    //    /// <param name="entity">entity should be a new class</param>
    //    Task UpdateBatchAsync(Expression<Func<T, bool>> predicate, T entity);

    //    Task DeleteAsync(T entity);
    //    Task DeleteRangeAsync(List<T> entities);
    //    Task DeleteAsync(Guid id);
    //    void DeleteRange(List<T> entities);
    //    /// <summary>
    //    /// To delete records without fetching.
    //    /// </summary>
    //    /// <param name="predicate">conditions to query data</param>
    //    void DeleteBatch(Expression<Func<T, bool>> predicate);
    //    /// <summary>
    //    /// To delete-async records without fetching.
    //    /// </summary>
    //    /// <param name="predicate">conditions to query data</param>
    //    Task DeleteBatchAsync(Expression<Func<T, bool>> predicate);

    //    void EntryModified(T entity);

    //    void SaveChanges();
    //    Task SaveChangesAsync();

    //    int Count();
    //    Task<int> CountAsync();
    //}

    //public interface IRepository<T>: IBaseRepository<T> where T : class {
    //    TResult GetById<TResult>(Guid id);
    //    Task<TResult> GetByIdAsync<TResult>(Guid id);
    //    Task<TResult> GetByIdAsync<TResult>(Guid id, Expression<Func<T, TResult>> selector);
    //}
}
