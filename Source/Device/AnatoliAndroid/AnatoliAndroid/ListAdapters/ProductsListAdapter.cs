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
                    }
                };
            }
            if (ShoppingCard.GetInstance().Items.ContainsKey(item.product_id))
            {
                _productCountTextView.Text = ShoppingCard.GetInstance().Items[item.product_id].Count.ToString();
            }
            else
                _productCountTextView.Text = "0";

            productAddButton.Click += (o, e) => productAddButton_Click(item);
            productRemoveButton.Click += (o, e) => productRemoveButton_Click(item);
            productNameTextView.Text = item.product_name;
            productPriceTextView.Text = string.Format(" {0}  Ê„«‰", item.price);
            // productIimageView.SetUrlDrawable(MadanerClient.Configuration.UsersImageBaseUri + "/" + item.User.image, null, 600000);
            return convertView;
        }

        private void productRemoveButton_Click(ProductModel item)
        {
            if (ShoppingCard.GetInstance().Items.ContainsKey(item.product_id))
            {
                if (ShoppingCard.GetInstance().Items[item.product_id].Count == 1)
                {
                    ShoppingCard.GetInstance().Items.Remove(item.product_id);
                    _productCountTextView.Text = "";
                }
                else
                {
                    ShoppingCard.GetInstance().Items[item.product_id].Count--;
                    _productCountTextView.Text = ShoppingCard.GetInstance().Items[item.product_id].Count.ToString();
                }
            }

            NotifyDataSetChanged();
            OnDataChanged();
        }

        void productAddButton_Click(ProductModel item)
        {
            // todo : implement add to shopping card

            if (ShoppingCard.GetInstance().Items.ContainsKey(item.product_id))
                (ShoppingCard.GetInstance().Items[item.product_id].Count)++;
            else
                ShoppingCard.GetInstance().Items.Add(item.product_id, new ShoppingCardItem(1, item));
            _productCountTextView.Text = ShoppingCard.GetInstance().Items[item.product_id].Count.ToString();
            NotifyDataSetChanged();
            OnDataChanged();
        }

        public void OnDataChanged()
        {
            if (DataChanged != null)
            {
                DataChanged(this);
            }
        }
        public event DataChangedEventHandler DataChanged;
        public delegate void DataChangedEventHandler(object sender);
    }
}