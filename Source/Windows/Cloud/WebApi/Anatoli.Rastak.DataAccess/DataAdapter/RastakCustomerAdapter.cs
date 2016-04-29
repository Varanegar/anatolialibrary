using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels.Base;
using Anatoli.Rastak.ViewModels.Order;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class RastakCustomerAdapter : RastakBaseAdapter
    {
        private static RastakCustomerAdapter instance = null;
        public static RastakCustomerAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakCustomerAdapter();
                }
                return instance;
            }
        }
        RastakCustomerAdapter() { }
        public int GetCustomerId(string UserId)
        {
            return new DataContext().GetValue<int>("select CustomerId from Customer where customerSiteUserId='" + UserId.ToString() + "'");
        }

        public bool IsCustomerValid(string UserId)
        {
            int count = new DataContext().GetValue<int>("select count(CustomerId) from Customer where customerSiteUserId='" + UserId.ToString() + "'");
            return (count > 0) ? true : false;
        }
        public void SetCustomerSiteUserId(string UserId, string Mobile)
        {
            try { 
            new DataContext().Execute("update Customer set customerSiteUserId ='" +  UserId + "' where mobile='" + Mobile + "'");
            }
            catch (Exception ex)
            {
                log.Error("save UserId to site data ", ex);
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
                    var data = context.All<CreateUserBindingModel>(RastakDBQuery.Instance.GetNewCustomers());
                    userList = data.ToList();
                }

                return userList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
    }
}
