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
    public class ProductAdapter : BaseAdapter
    {
        public static List<ProductViewModel> GetAllProducts(DateTime lastUpload)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ProductViewModel>(DBQuery.GetProduct() + where);
                data.ToList().ForEach(item =>
                {
                    var productSuppllier = context.All<SupplierViewModel>(DBQuery.GetProductSupplier(item.ID));
                    item.Suppliers = productSuppllier.ToList();
                    var productValue = context.All<CharValueViewModel>(DBQuery.GetProductCharValue(item.ID));
                    item.CharValues = productValue.ToList();
                });
                products = data.ToList();

            }
            return products;       
        }
        public static List<ProductGroupViewModel> GetAllProductGroups(DateTime lastUpload)
        {
            List<ProductGroupViewModel> productGroup = new List<ProductGroupViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                context.Execute(DBQuery.GetProductGroupData());
                var data = context.All<ProductGroupViewModel>(DBQuery.GetProductGroupTree());
                productGroup = data.ToList();
            }
            return productGroup;
        }
    }
}
