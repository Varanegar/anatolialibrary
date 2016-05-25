using System.Configuration;
using Anatoli.DMC.DataAccess.Helpers;
using Anatoli.ViewModels.BaseModels;
using NLog;
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
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

        protected DataContext GetDataContext(Transaction tran = Transaction.Begin)
        {
            return new DataContext("DMCConnectionString", tran);
        }


        protected static string GetConnectionString()
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["DMCConnectionString"];
            return mySetting.ToString();
        }


    }
}
