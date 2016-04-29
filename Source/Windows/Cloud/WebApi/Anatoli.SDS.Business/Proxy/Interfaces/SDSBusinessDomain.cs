using Anatoli.SDS.ViewModels;
using Anatoli.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.SDS.Business.Proxy.Interfaces
{
    public class SDSBusinessDomain<TSource, TOut>
        where TSource : SDSBaseViewModel, new()
        where TOut : BaseViewModel, new()
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    }
}
