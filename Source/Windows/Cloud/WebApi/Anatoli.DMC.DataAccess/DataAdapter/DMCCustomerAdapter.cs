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
