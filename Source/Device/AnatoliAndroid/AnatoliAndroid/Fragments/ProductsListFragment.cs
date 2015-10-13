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
        protected override List<Anatoli.Framework.AnatoliBase.Query.QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<Anatoli.Framework.AnatoliBase.Query.QueryParameter>();
            parameters.Add(new Query.SortParam("order_count", SortTypes.DESC));
            return parameters;
        }
    }
}