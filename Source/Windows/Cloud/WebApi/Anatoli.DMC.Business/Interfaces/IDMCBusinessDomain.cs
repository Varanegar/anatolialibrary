using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.DMC.Business.Proxy.Interfaces;
using Anatoli.DMC.DataAccess.DataAdapter;

namespace Anatoli.DMC.Business
{
    public interface IDMCBusinessDomain<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
    }
}
