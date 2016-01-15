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
        string cat_id = null;
        public override void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().ShowSearchIcon();
        }
        protected override List<QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            parameters.Add(new SortParam("order_count", SortTypes.DESC));
            var leftRight = CategoryManager.GetLeftRight(cat_id);
            if (leftRight != null)
            {
                parameters.Add(new GreaterFilterParam("cat_left", leftRight.left.ToString()));
                parameters.Add(new SmallerFilterParam("cat_right", leftRight.right.ToString()));
            }

            return parameters;
        }
        public async Task SetCatId(string id)
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