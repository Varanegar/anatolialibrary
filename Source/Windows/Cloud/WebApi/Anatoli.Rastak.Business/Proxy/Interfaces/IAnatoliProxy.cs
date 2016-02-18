using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.Rastak.DataAccess.Helpers.Entity;

namespace Anatoli.Rastak.Business.Proxy.Interfaces
{
    public interface IAnatoliProxy<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        TOut Convert(TSource data, RastakStoreConfigEntity storeConfig);
        TSource ReverseConvert(TOut data, RastakStoreConfigEntity storeConfig);
        List<TOut> Convert(List<TSource> data, RastakStoreConfigEntity storeConfig);
        List<TSource> ReverseConvert(List<TOut> data, RastakStoreConfigEntity storeConfig);
    }
}