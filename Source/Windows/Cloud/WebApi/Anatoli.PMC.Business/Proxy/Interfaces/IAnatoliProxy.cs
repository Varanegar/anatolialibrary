using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.PMC.DataAccess.Helpers.Entity;

namespace Anatoli.PMC.Business.Proxy.Interfaces
{
    public interface IAnatoliProxy<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        TOut Convert(TSource data, PMCStoreConfigEntity storeConfig);
        TSource ReverseConvert(TOut data, PMCStoreConfigEntity storeConfig);
        List<TOut> Convert(List<TSource> data, PMCStoreConfigEntity storeConfig);
        List<TSource> ReverseConvert(List<TOut> data, PMCStoreConfigEntity storeConfig);
    }
}