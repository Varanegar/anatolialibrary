using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Anatoli.App.Model.Product;

namespace AnatoliAndroid.ListAdapters
{
    class ProductsListAdapter : BaseListAdapter<ProductModel>
    {
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.ProductSummaryLayout, null);
            ProductModel item = null;
            if (_list != null)
                item = _list[position];
            else
                return convertView;
            TextView productNameTextView = convertView.FindViewById<TextView>(Resource.Id.productNameTextView);
            TextView productPriceTextView = convertView.FindViewById<TextView>(Resource.Id.productPriceTextView);
            ImageView productIimageView = convertView.FindViewById<ImageView>(Resource.Id.productSummaryImageView);
            productNameTextView.Text = item.product_name;
            productPriceTextView.Text = "1000";
            // productIimageView.SetUrlDrawable(MadanerClient.Configuration.UsersImageBaseUri + "/" + item.User.image, null, 600000);
            return convertView;
        }
    }
}