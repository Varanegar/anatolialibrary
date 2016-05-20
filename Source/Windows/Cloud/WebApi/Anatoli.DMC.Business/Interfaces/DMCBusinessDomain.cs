using Anatoli.DMC.ViewModels;
using Anatoli.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.Business.Proxy.Interfaces
{
    public class DMCBusinessDomain  <TSource, TOut>
        where TSource : DMCBaseViewModel, new()
        where TOut : BaseViewModel, new()
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

    }
}
