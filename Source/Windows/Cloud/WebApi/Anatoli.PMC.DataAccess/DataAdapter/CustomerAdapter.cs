using Anatoli.PMC.DataAccess.Helpers;
using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels.Base;
using Anatoli.PMC.ViewModels.Order;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.User;
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
            int result;
            using(var context = new DataContext())
            {
                result = context.GetValue<int>("select CustomerId from Customer where customerSiteUserId='" + UserId.ToString() + "'");
            }
            return result;
        }

        public bool IsCustomerValid(string UserId, string connectionString = null, string centerId = null)
        {
            int count = 0;
            string query = "select count(CustomerId) from Customer where customerSiteUserId='" + UserId.ToString() + "'";
            if (centerId == null)
            {
                using (var context = new DataContext())
                {
                    count = new DataContext().GetValue<int>(query);
                }
            }
            else
            {
                using (var context = new DataContext(centerId, connectionString, Transaction.No))
                {
                    count = new DataContext().GetValue<int>(query);
                }
            }

            return (count > 0) ? true : false;
        }
        public void SetCustomerSiteUserId(string UserId, string Mobile)
        {
            try {
                using (var context = new DataContext(Transaction.No))
                {
                    context.Execute("update Customer set customerSiteUserId ='" + UserId + "' where mobile='" + Mobile + "'");
                }
            }
            catch (Exception ex)
            {
                log.Error("save UserId to site data ", ex);
                throw ex;
            }
        }

        public long GetNewCustomerCode(string UserId)
        {
            long customerCode = new DataContext().GetValue<int>(@"declare @CustomerCode bigint
                set @CustomerCode = (SELECT isnull(MAX(CAST(CustomerCode AS int)),0)+ 1 as CustomerCode FROM Customer)
                select @CustomerCode");
            return customerCode;
        }

        public List<CreateUserBindingModel> GetNewUsers(DateTime lastUpload)
        {
            try
            {
                List<CreateUserBindingModel> userList = new List<CreateUserBindingModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<CreateUserBindingModel>(DBQuery.Instance.GetNewCustomers());
                    userList = data.ToList();
                }

                return userList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                throw ex;
            }
        }
    }
}
