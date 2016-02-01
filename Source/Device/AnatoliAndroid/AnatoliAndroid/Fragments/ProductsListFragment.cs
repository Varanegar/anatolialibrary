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
using Anatoli.Framework.DataAdapter;
using Android.Animation;
using Android.Views.Animations;

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
        public override async Task Search(DBQuery query, string value)
        {
            await base.Search(query, value);
            var q2 = new StringQuery(string.Format("SELECT * FROM categories WHERE cat_name LIKE '%{0}%'", value));
            var groups = await BaseDataAdapter<CategoryInfoModel>.GetListAsync(q2);
            List<ProductModel> pl = new List<ProductModel>();
            foreach (var item in groups)
            {
                var p = new ProductModel();
                p.cat_id = item.cat_id;
                p.product_name = item.cat_name;
                p.is_group = 1;
                p.message = "group";
                p.image = (await CategoryManager.GetCategoryInfo(item.cat_id)).cat_image;
                pl.Add(p);
            }
            _listAdapter.List.InsertRange(0, pl);
        }
        public async Task SetCatId(string id)
        {
            var leftRight = CategoryManager.GetLeftRight(id);
            StringQuery query;
            if (leftRight != null)
                query = new StringQuery(string.Format("SELECT * FROM products_price_view WHERE cat_left >= {0} AND cat_right <= {1} ORDER BY cat_id ", leftRight.left, leftRight.right));
            else
                query = new StringQuery(string.Format("SELECT * FROM products_price_view ORDER BY cat_id"));
            _dataManager.SetQueries(query, null);
            try
            {
                _listAdapter.List = await _dataManager.GetNextAsync();
                _listAdapter.NotifyDataSetChanged();
                _listView.SetSelection(0);
            }
            catch (Exception)
            {

            }
        }
    }
}