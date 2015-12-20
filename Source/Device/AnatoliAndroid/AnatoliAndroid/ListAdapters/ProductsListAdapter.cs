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
using System.Threading.Tasks;
using FortySevenDeg.SwipeListView;
using AnatoliAndroid.Activities;
using AnatoliAndroid.Fragments;
using Android.Content.Res;

namespace AnatoliAndroid.ListAdapters
{
    class ProductsListAdapter : BaseSwipeListAdapter<ProductManager, ProductModel>
    {
        TextView _productCountTextView;
        TextView _productNameTextView;
        TextView _productPriceTextView;
        TextView _bproductNameTextView;
        TextView _favoritsTextView;
        ImageView _productIimageView;
        ImageView _productAddButton;
        ImageView _bproductImageView;
        ImageButton _favoritsButton;
        ImageView _productRemoveButton;
        ImageButton _removeAllProductsButton;
        OnTouchListener _addTouchlistener;
        LinearLayout _counterLinearLayout;
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {

            var view = (convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ProductSummaryLayout, null));

            ProductModel item = null;
            if (List != null)
                item = this[position];
            else
                return view;
            if (convertView == null)
            {
                _productNameTextView = view.FindViewById<TextView>(Resource.Id.productNameTextView);
                _favoritsTextView = view.FindViewById<TextView>(Resource.Id.favoritsTextView);
                _bproductNameTextView = view.FindViewById<TextView>(Resource.Id.bproductNameTextView);
                _productPriceTextView = view.FindViewById<TextView>(Resource.Id.productPriceTextView);
                _productCountTextView = view.FindViewById<TextView>(Resource.Id.productCountTextView);
                _productIimageView = view.FindViewById<ImageView>(Resource.Id.productSummaryImageView);
                _bproductImageView = view.FindViewById<ImageView>(Resource.Id.bproductImageView);
                _productAddButton = view.FindViewById<ImageView>(Resource.Id.addProductImageView);
                _productRemoveButton = view.FindViewById<ImageView>(Resource.Id.removeProductImageView);
                _removeAllProductsButton = view.FindViewById<ImageButton>(Resource.Id.removeAllProductsButton);
                _favoritsButton = view.FindViewById<ImageButton>(Resource.Id.favoritsButton);
                _counterLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.counterLinearLayout);


                view.SetTag(Resource.Id.productPriceTextView, _productPriceTextView);
                view.SetTag(Resource.Id.removeProductImageView, _productRemoveButton);
                view.SetTag(Resource.Id.favoritsTextView, _favoritsTextView);
                view.SetTag(Resource.Id.addProductImageView, _productAddButton);
                view.SetTag(Resource.Id.productSummaryImageView, _productIimageView);
                view.SetTag(Resource.Id.bproductImageView, _bproductImageView);
                view.SetTag(Resource.Id.productCountTextView, _productCountTextView);
                view.SetTag(Resource.Id.productNameTextView, _productNameTextView);
                view.SetTag(Resource.Id.bproductNameTextView, _bproductNameTextView);
                view.SetTag(Resource.Id.removeAllProductsButton, _removeAllProductsButton);
                view.SetTag(Resource.Id.favoritsButton, _favoritsButton);
                view.SetTag(Resource.Id.counterLinearLayout, _counterLinearLayout);

            }
            else
            {
                _productCountTextView = (TextView)view.GetTag(Resource.Id.productCountTextView);
                _favoritsTextView = (TextView)view.GetTag(Resource.Id.favoritsTextView);
                _productNameTextView = (TextView)view.GetTag(Resource.Id.productNameTextView);
                _bproductNameTextView = (TextView)view.GetTag(Resource.Id.bproductNameTextView);
                _productRemoveButton = (ImageView)view.GetTag(Resource.Id.removeProductImageView);
                _productAddButton = (ImageView)view.GetTag(Resource.Id.addProductImageView);
                _productIimageView = (ImageView)view.GetTag(Resource.Id.productSummaryImageView);
                _bproductImageView = (ImageView)view.GetTag(Resource.Id.bproductImageView);
                _productPriceTextView = (TextView)view.GetTag(Resource.Id.productPriceTextView);
                _removeAllProductsButton = (ImageButton)view.GetTag(Resource.Id.removeAllProductsButton);
                _favoritsButton = (ImageButton)view.GetTag(Resource.Id.favoritsButton);
                _counterLinearLayout = (LinearLayout)view.GetTag(Resource.Id.counterLinearLayout);
            }

            if (!String.IsNullOrEmpty(item.image))
            {
                UrlImageViewHelper.SetUrlDrawable(_productIimageView, item.image, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
                UrlImageViewHelper.SetUrlDrawable(_bproductImageView, item.image, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
            }
            else
            {
                _productIimageView.SetImageResource(Resource.Drawable.igmart);
                _bproductImageView.SetImageResource(Resource.Drawable.igmart);
            }


            if (item.IsFavorit)
            {
                _favoritsTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.RemoveFromList);
            }
            else
            {
                _favoritsTextView.Text = AnatoliApp.GetResources().GetText(Resource.String.AddToList);
            }

            _productCountTextView.Text = item.count.ToString() + " عدد";
            _productNameTextView.Text = item.product_name;
            _bproductNameTextView.Text = item.product_name;
            _productPriceTextView.Text = string.Format(" {0} تومان", item.price);

            if (item.product_name.Equals(_productNameTextView.Text))
            {
                if (item.count > 0)
                    _counterLinearLayout.Visibility = ViewStates.Visible;
                else
                    _counterLinearLayout.Visibility = ViewStates.Gone;
            }

            var removeAll = new OnTouchListener();
            _removeAllProductsButton.SetOnTouchListener(removeAll);
            removeAll.Click += async (s, e) =>
            {
                OnBackClicked(position);
                int a = await ShoppingCardManager.GetItemsCountAsync();
                TextView counter = AnatoliApp.GetInstance().ShoppingCardItemCount;
                double p = AnatoliApp.GetInstance().GetTotalPrice();
                if (await ShoppingCardManager.RemoveProductAsync(item, true))
                {
                    while (item.count > 0)
                    {
                        await Task.Delay(150);
                        item.count--;
                        counter.Text = (--a).ToString();
                        p = p - item.price;
                        AnatoliApp.GetInstance().SetTotalPrice(p);
                        NotifyDataSetChanged();
                        OnDataChanged();
                    }
                    if (item.product_name.Equals(_productNameTextView.Text))
                        _counterLinearLayout.Visibility = ViewStates.Gone;
                    NotifyDataSetChanged();
                    OnDataChanged();
                    OnShoppingCardItemRemoved(item);
                    Toast.MakeText(AnatoliApp.GetInstance().Activity, Resource.String.ItemRemoved, ToastLength.Short).Show();
                }
            };



            var _favoritsTouchlistener = new OnTouchListener();
            _favoritsButton.SetOnTouchListener(_favoritsTouchlistener);
            _favoritsTouchlistener.Click += async (s, e) =>
            {
                if (this[position].IsFavorit)
                {
                    if (await ProductManager.RemoveFavorit(this[position].product_id) == true)
                    {
                        this[position].favorit = 0;
                        NotifyDataSetChanged();
                        OnFavoritRemoved(this[position]);
                    }
                }
                else
                {
                    if (await ProductManager.AddToFavorits(this[position].product_id) == true)
                    {
                        this[position].favorit = 1;
                        NotifyDataSetChanged();
                        OnFavoritAdded(this[position]);
                    }
                }
            };

            _addTouchlistener = new OnTouchListener();
            _productAddButton.SetOnTouchListener(_addTouchlistener);
            _addTouchlistener.Click += async (s, e) =>
            {
                if (await ShoppingCardManager.AddProductAsync(item))
                {
                    item.count++;
                    if (item.product_name.Equals(_productNameTextView.Text))
                        if (item.count == 1)
                        {
                            _counterLinearLayout.Visibility = ViewStates.Visible;
                        }
                    NotifyDataSetChanged();
                    OnDataChanged();
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().SetTotalPrice(await ShoppingCardManager.GetTotalPriceAsync());
                }

            };

            var _removeTouchlistener = new OnTouchListener();
            _productRemoveButton.SetOnTouchListener(_removeTouchlistener);
            _removeTouchlistener.Click += async (s, e) =>
            {
                if (await ShoppingCardManager.RemoveProductAsync(item))
                {
                    item.count--;
                    if (item.count == 0)
                    {
                        if (item.product_name.Equals(_productNameTextView.Text))
                            _counterLinearLayout.Visibility = ViewStates.Gone;
                        OnShoppingCardItemRemoved(item);
                    }
                    NotifyDataSetChanged();
                    OnDataChanged();
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().SetTotalPrice(await ShoppingCardManager.GetTotalPriceAsync());
                }
            };


            return view;
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