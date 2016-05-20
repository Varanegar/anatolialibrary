using System.Data;
using Anatoli.DMC.DataAccess.Helpers;
using Anatoli.DMC.DataAccess.Helpers.Entity;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Base;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.DataAdapter
{
    public class DMCCustomerAdapter : DMCBaseAdapter
    {
        private static DMCCustomerAdapter instance = null;
        public static DMCCustomerAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DMCCustomerAdapter();
                }
                return instance;
            }
        }
        DMCCustomerAdapter() { }
        public int GetCustomerId(string UserId)
        {
            int result;
            using(var context = GetDataContext(Transaction.No))
            {
                result = context.GetValue<int>("select CustomerId from Customer where customerSiteUserId='" + UserId.ToString() + "'");
            }
            return result;
        }

        public bool IsCustomerValid(string UserId)
        {
            int count = 0;
            using (var context = GetDataContext(Transaction.No))
            {
                count = context.GetValue<int>("select count(CustomerId) from Customer where customerSiteUserId='" + UserId.ToString() + "'");
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
            using (var context = new DataContext(Transaction.No))
            {
                long customerCode = context.GetValue<int>(@"declare @CustomerCode bigint
                set @CustomerCode = (SELECT isnull(MAX(CAST(CustomerCode AS int)),0)+ 1 as CustomerCode FROM Customer)
                select @CustomerCode");
                return customerCode;
            }
        }

        public List<CreateUserBindingModel> GetNewUsers(DateTime lastUpload)
        {
            try
            {
                List<CreateUserBindingModel> userList = new List<CreateUserBindingModel>();
                using (var context = GetDataContext(Transaction.No))
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

        #region gis

        public bool UpdateCustomerLatLng(DMCCustomerPointViewModel customerPoint)
        {
            try
            {
                using (var context = new DataContext(Transaction.No))
                {
                    context.Execute("UPDATE Customer " +
                                    "SET  [Latitude] = " + customerPoint.Latitude +"* 1000000,"+
                                         "[Longitude] = " + customerPoint.Longitude + "* 1000000 " +
                                    "WHERE UniqueId='" + customerPoint.CustomerUniqueId + "'");
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Failed update data ", ex);
                return false;
            }            
        }

        public List<DMCCustomerViewModel> LoadCustomerBySearchTerm(string searchStr)
        {
            using (var context = GetDataContext(Transaction.No))
            {
                var list = 
                    context.All<DMCCustomerViewModel>("SELECT	customer.UniqueId, "+
                                    "[Longitude],[Latitude],"+
				                    "[CustomerName], [CustomerCode], [StoreName], Phone "+
                                    "FROM customer "+
                                    "WHERE ( [CustomerName] +' '+[CustomerCode] +' '+ [StoreName] like '%" + searchStr + "%') "
                    
                    ).ToList();
                return list;
            }
        }

        #endregion


    }
}
