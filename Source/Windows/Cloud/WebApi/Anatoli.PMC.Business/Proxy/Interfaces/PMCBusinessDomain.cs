using Anatoli.PMC.ViewModels;
using Anatoli.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.Business.Proxy.Interfaces
{
    public class PMCBusinessDomain<TSource, TOut>
        where TSource : PMCBaseViewModel, new()
        where TOut : BaseViewModel, new()
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected IAnatoliProxy<TSource, TOut> Proxy { get; set; }

    }
}
