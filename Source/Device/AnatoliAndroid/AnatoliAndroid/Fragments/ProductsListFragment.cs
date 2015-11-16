using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Anatoli.App.Model.Product;
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("دسته بندی کالا")]
    class ProductsListFragment : BaseListFragment<ProductManager, ProductsListAdapter, NoListToolsDialog, ProductModel>
    {
        int cat_id = 0;
        protected override List<QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            parameters.Add(new SortParam("order_count", SortTypes.DESC));
            ProductManager pm = new ProductManager();
            var ids = CategoryManager.GetCategories(cat_id);
            parameters.Add(new CategoryFilterParam("cat_id", cat_id.ToString()));
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    parameters.Add(new CategoryFilterParam("cat_id", item.catId.ToString()));
                }
            }

            return parameters;
        }
        public async void SetCatId(int id)
        {
            cat_id = id;
            SetParameters();
            _listAdapter.List = await _dataManager.GetNextAsync();
            _listAdapter.NotifyDataSetChanged();
        }

        protected override string GetTableName()
        {
            return "products_price_view";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Products.ProductsView;
        }
    }
}