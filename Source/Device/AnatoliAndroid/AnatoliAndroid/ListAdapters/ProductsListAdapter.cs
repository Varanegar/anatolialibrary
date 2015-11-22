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
using Koush;


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
            if (!String.IsNullOrEmpty(item.image))
                UrlImageViewHelper.SetUrlDrawable(productIimageView, item.image, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
            else
                productIimageView.SetImageResource(Resource.Drawable.igmart);
            Button productAddButton = convertView.FindViewById<Button>(Resource.Id.addProductButton);
            Button productRemoveButton = convertView.FindViewById<Button>(Resource.Id.removeProductButton);
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
                        OnFavoritRemoved(item);
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
                        OnFavoritAdded(item);
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
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                }
            };
            productRemoveButton.Click += async (o, e) =>
                {
                    if (await ShoppingCardManager.RemoveProductAsync(item))
                    {
                        item.count--;
                        if (item.count == 0)
                        {
                            OnShoppingCardItemRemoved(item);
                        }
                        NotifyDataSetChanged();
                        OnDataChanged();
                        AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                    }
                };
            productRemoveButton.LongClick += async (s, e) =>
                {
                    int a = await ShoppingCardManager.GetItemsCountAsync();
                    if (await ShoppingCardManager.RemoveProductAsync(item, true))
                    {
                        OnShoppingCardItemRemoved(item);
                        int b = await ShoppingCardManager.GetItemsCountAsync();
                        for (int i = a; i >= b; i--)
                        {
                            await System.Threading.Tasks.Task.Delay(100);
                            AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = i.ToString();
                            if (item.count > 0)
                            {
                                item.count--;
                                NotifyDataSetChanged();
                                OnDataChanged();
                            }

                        }
                    }
                };

            productNameTextView.Text = item.product_name;
            productPriceTextView.Text = string.Format(" {0} تومان", item.price);
            return convertView;
        }



        void OnShoppingCardItemRemoved(ProductModel data)
        {
            if (ShoppingCardItemRemoved != null)
            {
                ShoppingCardItemRemoved(this, data);
            }
        }
        public event ItemRemovedEventHandler ShoppingCardItemRemoved;
        public delegate void ItemRemovedEventHandler(object sender, ProductModel data);

        void OnShoppingCardItemAdded(ProductModel data)
        {
            if (ShoppingCardItemAdded != null)
            {
                ShoppingCardItemAdded(this, data);
            }
        }
        public event ItemAddedEventHandler ShoppingCardItemAdded;
        public delegate void ItemAddedEventHandler(object sender, ProductModel data);

        void OnFavoritRemoved(ProductModel data)
        {
            if (FavoritRemoved != null)
            {
                FavoritRemoved(this, data);
            }
        }
        public event FavoritRemovedEventHandler FavoritRemoved;
        public delegate void FavoritRemovedEventHandler(object sender, ProductModel data);

        void OnFavoritAdded(ProductModel data)
        {
            if (FavoritAdded != null)
            {
                FavoritAdded(this, data);
            }
        }
        public event FavoritAddedEventHandler FavoritAdded;
        public delegate void FavoritAddedEventHandler(object sender, ProductModel data);
    }
}