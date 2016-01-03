using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.Base;
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
        public int GetCustomerId(string UserId)
        {
            return new DataContext().GetValue<int>("select CustomerId from Customer where customerSiteUserId='" + UserId.ToString() + "'");
        }

        public bool IsCustomerValid(string UserId)
        {
            int count = new DataContext().GetValue<int>("select count(CustomerId) from Customer where customerSiteUserId='" + UserId.ToString() + "'");
            return (count > 0) ? true : false;
        }

        public long GetNewCustomerCode(string UserId)
        {
            long customerCode = new DataContext().GetValue<int>(@"declare @CustomerCode bigint
                set @CustomerCode = (SELECT isnull(MAX(CAST(CustomerCode AS int)),0)+ 1 as CustomerCode FROM Customer)
                select @CustomerCode");
            return customerCode;
        }
    }
}
