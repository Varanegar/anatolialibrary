using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.Rastak.DataAccess.Helpers;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class ProductAdapter : BaseAdapter
    {
        private static ProductAdapter instance = null;
        public static ProductAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductAdapter();
                }
                return instance;
            }
        }
        ProductAdapter() { }

        public int GetProductId(Guid productUniqueId)
        {
            return new DataContext().GetValue<int>("select productId from Product where uniqueId='" + productUniqueId.ToString() + "'");
        }
        public string GetProductUniqueId(int productId)
        {
            return new DataContext().GetValue<string>("select uniqueId from Product where productId='" + productId + "'");
        }
        public List<ProductViewModel> GetAllProducts(DateTime lastUpload)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ProductViewModel>(DBQuery.Instance.GetProduct() + where);
                data.ToList().ForEach(item =>
                {
                    var productSuppllier = context.All<SupplierViewModel>(DBQuery.Instance.GetProductSupplier(item.ID));
                    item.Suppliers = productSuppllier.ToList();
                    var productValue = context.All<CharValueViewModel>(DBQuery.Instance.GetProductCharValue(item.ID));
                    item.CharValues = productValue.ToList();
                });
                products = data.ToList();

            }
            return products;       
        }
        public List<ProductGroupViewModel> GetAllProductGroups(DateTime lastUpload)
        {
            List<ProductGroupViewModel> productGroup = new List<ProductGroupViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                context.Execute(DBQuery.Instance.GetProductGroupData());
                var data = context.All<ProductGroupViewModel>(DBQuery.Instance.GetProductGroupTree());
                productGroup = data.ToList();
            }
            return productGroup;
        }
        public List<MainProductGroupViewModel> GetAllMainProductGroups(DateTime lastUpload)
        {
            List<MainProductGroupViewModel> productGroup = new List<MainProductGroupViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                context.Execute(DBQuery.Instance.GetMainProductGroupData());
                var data = context.All<MainProductGroupViewModel>(DBQuery.Instance.GetMainProductGroupTree());
                productGroup = data.ToList();
            }
            return productGroup;
        }
    }
}
