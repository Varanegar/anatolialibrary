using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class CustomerAdapter : BaseAdapter
    {
        private static CustomerAdapter instance = null;
        public static CustomerAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerAdapter();
                }
                return instance;
            }
        }
        CustomerAdapter() { }
        public int GetCustomerId(Guid UserId)
        {
            return new DataContext().GetValue<int>("select CustomerId from Customer where customerSiteUserId='" + UserId.ToString() + "'");
        }

    }
}
