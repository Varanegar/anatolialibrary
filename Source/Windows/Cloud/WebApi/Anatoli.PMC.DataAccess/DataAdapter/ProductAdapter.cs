﻿using Anatoli.ViewModels.ProductModels;
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
            int result;
            using (var context = new DataContext())
            {
                result = context.GetValue<int>("select productId from Product where uniqueId='" + productUniqueId.ToString() + "'");
            }
            return result;
        }
        public string GetProductUniqueId(int productId)
        {
            string result = "";
            using (var context = new DataContext())
            {
                result = context.GetValue<string>("select uniqueId from Product where productId='" + productId + "'");
            }
            return result;
        }
        public List<ProductViewModel> GetAllProducts(DateTime lastUpload)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ProductViewModel>(DBQuery.Instance.GetProduct() + where);
                products = data.ToList();

            }
            return products;       
        }
        public List<ProductSupplierViewModel> GetAllProductSuppliers(DateTime lastUpload)
        {
            List<ProductSupplierViewModel> products = new List<ProductSupplierViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ProductSupplierViewModel>(DBQuery.Instance.GetProductSupplier() + where);
                products = data.ToList();

            }
            return products;
        }
        public List<ProductCharValueViewModel> GetAllProductCharValues(DateTime lastUpload)
        {
            List<ProductCharValueViewModel> products = new List<ProductCharValueViewModel>();
            using (var context = new DataContext())
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " where p.ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                var data = context.All<ProductCharValueViewModel>(DBQuery.Instance.GetProductCharValue() + where);
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
