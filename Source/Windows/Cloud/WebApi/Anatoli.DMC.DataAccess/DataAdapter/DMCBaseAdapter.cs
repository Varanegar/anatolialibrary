using Anatoli.DMC.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public abstract class DMCBaseAdapter
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected DataContext GetDataContext(Transaction tran = Transaction.Begin)
        {
            return new DataContext("DMCConnectionString", tran);
        }
    }
}
