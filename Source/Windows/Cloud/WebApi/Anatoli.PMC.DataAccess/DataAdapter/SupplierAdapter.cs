using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.PMC.DataAccess.Helpers;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class SupplierAdapter : BaseAdapter
    {
        public static List<SupplierViewModel> GetAllSuppliers(DateTime lastUpload)
        {
            try
            {
                List<SupplierViewModel> supplier = new List<SupplierViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " where ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    var data = context.All<SupplierViewModel>(DBQuery.GetSupplir() + where);
                    supplier = data.ToList();
                }
                return supplier;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }

        }
    }
}
