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
        private static SupplierAdapter instance = null;
        public static SupplierAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SupplierAdapter();
                }
                return instance;
            }
        }
        public  List<SupplierViewModel> GetAllSuppliers(DateTime lastUpload)
        {
            try
            {
                List<SupplierViewModel> supplier = new List<SupplierViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " where ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    var data = context.All<SupplierViewModel>(DBQuery.Instance.GetSupplier() + where);
                    supplier = data.ToList();
                }
                return supplier;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                throw ex;
            }

        }
    }
}
