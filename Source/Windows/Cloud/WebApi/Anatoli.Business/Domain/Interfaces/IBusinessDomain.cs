using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models;
using Anatoli.ViewModels;
using System.Linq.Expressions;

namespace Anatoli.Business
{
    public interface IBusinessDomain<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        IAnatoliProxy<TSource, TOut> Proxy { get; set; }
        IRepository<TSource> Repository { get; set; }
        Guid ApplicationOwnerId { get; }

        Task<List<TOut>> GetAll();
        Task<List<TOut>> GetAllChangedAfter(DateTime selectedDate);
        Task<List<TOut>> PublishAsync(List<TOut> ProductViewModels);
        //void Publish(List<TOut> ProductViewModels);
        Task<List<TOut>> Delete(List<TOut> ProductViewModels);
    }

    public interface IBusinessDomainV2<TMainSource, TMainSourceView>
        where TMainSource : BaseModel, new()
        where TMainSourceView : BaseViewModel, new()
    {
        Guid ApplicationOwnerKey { get; }
        Guid DataOwnerKey { get; }
        Guid DataOwnerCenterKey { get; }

        Task<List<TMainSourceView>> GetAllAsync(Expression<Func<TMainSource, bool>> predicate);
        Task<List<TMainSourceView>> GetAllAsync();
        Task<TMainSourceView> GetByIdAsync(Guid id);
        Task<List<TMainSourceView>> GetAllChangedAfterAsync(DateTime selectedDate);
        Task PublishAsync(List<TMainSource> data);
        Task PublishAsync(TMainSource data);
        Task DeleteAsync(List<TMainSource> data);
        Task DeleteAsync(List<TMainSourceView> data);
        Task CheckDeletedAsync(List<TMainSourceView> data);
    }

    public interface IBusinessDomainV3<TSource> where TSource : BaseModel, new()
    {
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TResult>> selector);

        Task<TResult> GetByIdAsync<TResult>(Guid id);

        Task<List<TResult>> GetAllChangedAfterAsync<TResult>(DateTime selectedDate);

        Task PublishAsync(List<TSource> data);
        Task PublishAsync(TSource data);

        Task DeleteAsync(List<TSource> data);
        Task DeleteAsync<TResult>(List<TResult> data) where TResult : BaseViewModel;
        Task CheckDeletedAsync<TResult>(List<TResult> data) where TResult : BaseViewModel, new();
    }
}
