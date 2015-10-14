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
    class ProductsListFragment : BaseListFragment<ProductManager, ProductsListAdapter, ProductModel>
    {
        int cat_id = 0;
        protected override List<Anatoli.Framework.AnatoliBase.Query.QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<Anatoli.Framework.AnatoliBase.Query.QueryParameter>();
            parameters.Add(new Query.SortParam("order_count", SortTypes.DESC));
            ProductManager pm = new ProductManager();
            var ids = pm.GetCategories(cat_id);
            parameters.Add(new Query.FilterParam("cat_id", cat_id.ToString()));
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    parameters.Add(new Query.FilterParam("cat_id", item.Item1.ToString()));
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
    }
}