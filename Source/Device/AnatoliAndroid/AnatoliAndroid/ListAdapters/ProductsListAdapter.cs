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
        TextView _productCountTextView;
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
            Button productAddButton = convertView.FindViewById<Button>(Resource.Id.addProductButton);
            Button productRemoveButton = convertView.FindViewById<Button>(Resource.Id.removeProductButton);

            if (ShoppingCard.GetInstance().Items.ContainsKey(item.product_id))
            {
                _productCountTextView.Text = ShoppingCard.GetInstance().Items[item.product_id].ToString();
            }
            else
                _productCountTextView.Text = "";

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
                if (ShoppingCard.GetInstance().Items[item.product_id] == 1)
                {
                    ShoppingCard.GetInstance().Items.Remove(item.product_id);
                    _productCountTextView.Text = "";
                }
                else
                {
                    ShoppingCard.GetInstance().Items[item.product_id]--;
                    _productCountTextView.Text = ShoppingCard.GetInstance().Items[item.product_id].ToString();
                }
            }

            NotifyDataSetChanged();
            OnDataChanged();
        }

        void productAddButton_Click(ProductModel item)
        {
            // todo : implement add to shopping card

            if (ShoppingCard.GetInstance().Items.ContainsKey(item.product_id))
                ShoppingCard.GetInstance().Items[item.product_id]++;
            else
                ShoppingCard.GetInstance().Items.Add(item.product_id, 1);
            _productCountTextView.Text = ShoppingCard.GetInstance().Items[item.product_id].ToString();
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