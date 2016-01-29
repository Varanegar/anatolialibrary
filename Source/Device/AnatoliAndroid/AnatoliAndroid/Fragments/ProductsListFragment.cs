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
using AnatoliAndroid.Activities;
using System.Threading.Tasks;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("دسته بندی کالا")]
    class ProductsListFragment : BaseSwipeListFragment<ProductManager, ProductsListAdapter, NoListToolsDialog, ProductModel>
    {
        public ProductsListFragment()
        {
            var query = new StringQuery("SELECT * FROM products_price_view ORDER BY cat_id");
            _dataManager.SetQueries(query, null);
        }
        public override void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().ShowSearchIcon();
        }
        public async Task SetCatId(string id)
        {
            var leftRight = CategoryManager.GetLeftRight(id);
            StringQuery query;
            if (leftRight != null)
                query = new StringQuery(string.Format("SELECT * FROM products_price_view WHERE cat_left > {0} AND cat_right < {1} ORDER BY cat_id ", leftRight.left, leftRight.right));
            else
                query = new StringQuery(string.Format("SELECT * FROM products_price_view ORDER BY cat_id"));
            _dataManager.SetQueries(query, null);
            try
            {
                _listAdapter.List = await _dataManager.GetNextAsync();
                _listAdapter.NotifyDataSetChanged();
            }
            catch (Exception)
            {

            }
        }
    }
}