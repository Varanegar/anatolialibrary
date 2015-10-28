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
using Anatoli.Framework.Manager;
using Anatoli.Framework.DataAdapter;

namespace AnatoliAndroid.Fragments
{
    class FavoritsListFragment : BaseListFragment<ProductManager, ProductsListAdapter, ProductModel>
    {
        public FavoritsListFragment()
        {
            _listAdapter.DataRemoved += _listAdapter_DataRemoved;
        }

        void _listAdapter_DataRemoved(object sender, ProductModel item)
        {
            _listAdapter.List.Remove(item);
            _listView.InvalidateViews();
        }

        protected override List<QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            parameters.Add(new SearchFilterParam("favorit", "1"));
            return parameters;
        }

        protected override string GetTableName()
        {
            return "products_price_view";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Products.FavoritsView;
        }
    }
}