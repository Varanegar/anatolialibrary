using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;

namespace Anatoli.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        AnatoliDbContext DbContext { get; set; }
        IQueryable<T> GetQuery();
        Task<T> GetByIdAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Guid id);
        void EntryModified(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
        Task<int> CountAsync();
    }
}
