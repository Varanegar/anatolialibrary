using Anatoli.PMC.ViewModels;
using Anatoli.ViewModels;
using NLog;
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
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

        protected IAnatoliProxy<TSource, TOut> Proxy { get; set; }

    }
}
