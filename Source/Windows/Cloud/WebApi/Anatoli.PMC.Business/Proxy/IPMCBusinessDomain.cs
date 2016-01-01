using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.PMC.Business.Proxy.Interfaces;
using Anatoli.PMC.DataAccess.DataAdapter;

namespace Anatoli.PMC.Business
{
    public interface IPMCBusinessDomain<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        IAnatoliProxy<TSource, TOut> Proxy { get; set; }

        List<TOut> GetAll();
        List<TOut> GetAllChangedAfter(DateTime selectedDate);
        Task PublishAsync(List<TOut> BaseViewModels);
        Task Delete(List<TOut> BaseViewModels);
    }
}
