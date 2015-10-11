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
using Anatoli.App.Adapter;
using Anatoli.App.Model.Product;
using Anatoli.App.Manager;

namespace AnatoliAndroid.Fragments
{
    class ProductsListFragment : BaseListFragment<ProductAdapter, ProductListModel, ProductModel>
    {
        public ProductsListFragment() : base(new ProductManager()) { }
    }
}