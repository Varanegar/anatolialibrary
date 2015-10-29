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
using Anatoli.App.Model.AnatoliUser;
using Anatoli.App.Manager;

namespace AnatoliAndroid.ListAdapters
{
    class ProductsListAdapter : BaseListAdapter<ProductManager, ProductModel>
    {
        TextView _productCountTextView;
        ImageView _favoritsImageView;

        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.ProductSummaryLayout, null);
            ProductModel item = null;
            if (List != null)
                item = List[position];
            else
                return convertView;
            TextView productNameTextView = convertView.FindViewById<TextView>(Resource.Id.productNameTextView);
            TextView productPriceTextView = convertView.FindViewById<TextView>(Resource.Id.productPriceTextView);
            _productCountTextView = convertView.FindViewById<TextView>(Resource.Id.productCountTextView);
            ImageView productIimageView = convertView.FindViewById<ImageView>(Resource.Id.productSummaryImageView);
            ImageView productAddButton = convertView.FindViewById<ImageView>(Resource.Id.addProductImageView);
            ImageView productRemoveButton = convertView.FindViewById<ImageView>(Resource.Id.removeProductImageView);
            _favoritsImageView = convertView.FindViewById<ImageView>(Resource.Id.addToFavoritsImageView);

            if (item.IsFavorit)
            {
                _favoritsImageView.SetImageResource(Resource.Drawable.Favorits);
                _favoritsImageView.Click += async (s, e) =>
                {
                    if (await ProductManager.RemoveFavorit(item.product_id) == true)
                    {
                        item.favorit = 0;
                        NotifyDataSetChanged();
                        OnDataRemoved(item);
                    }
                };
            }
            else
            {
                _favoritsImageView.Click += async (s, e) =>
                {
                    if (await ProductManager.AddToFavorits(item) == true)
                    {
                        item.favorit = 1;
                        NotifyDataSetChanged();
                        OnDataRemoved(item);
                    }
                };
            }
            _productCountTextView.Text = item.count.ToString();

            productAddButton.Click += async (o, e) =>
            {
                if (await ShoppingCardManager.AddProductAsync(item))
                {
                    item.count++;
                    NotifyDataSetChanged();
                    OnDataChanged();
                }
            };
            productRemoveButton.Click += async (o, e) =>
                {
                    if (await ShoppingCardManager.RemoveProductAsync(item))
                    {
                        item.count--;
                        if (item.count == 0)
                        {
                            OnDataRemoved(item);
                        }
                        NotifyDataSetChanged();
                        OnDataChanged();
                    }
                };
            productNameTextView.Text = item.product_name;
            productPriceTextView.Text = string.Format(" {0}  Ê„«‰", item.price);
            return convertView;
        }
    }
}