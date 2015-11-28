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


namespace AnatoliAndroid.ListAdapters
{
    class ProductsListAdapter : BaseListAdapter<ProductManager, ProductModel>
    {
        TextView _productCountTextView;
        public ImageView _favoritsImageView;
        TextView _productNameTextView;
        TextView _productPriceTextView;
        ImageView _productIimageView;
        Button _productAddButton;
        Button _productRemoveButton;
        OnTouchListener _addTouchlistener;
        RelativeLayout _productSummaryRelativeLayout;
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
                _productPriceTextView = view.FindViewById<TextView>(Resource.Id.productPriceTextView);
                _productCountTextView = view.FindViewById<TextView>(Resource.Id.productCountTextView);
                _productIimageView = view.FindViewById<ImageView>(Resource.Id.productSummaryImageView);
                _productAddButton = view.FindViewById<Button>(Resource.Id.addProductButton);
                _productRemoveButton = view.FindViewById<Button>(Resource.Id.removeProductButton);
                _favoritsImageView = view.FindViewById<ImageView>(Resource.Id.addToFavoritsImageView);
                _productSummaryRelativeLayout = view.FindViewById<RelativeLayout>(Resource.Id.productSummaryRelativeLayout);

                


                view.SetTag(Resource.Id.addToFavoritsImageView, _favoritsImageView);
                view.SetTag(Resource.Id.productPriceTextView, _productPriceTextView);
                view.SetTag(Resource.Id.removeProductButton, _productRemoveButton);
                view.SetTag(Resource.Id.addProductButton, _productAddButton);
                view.SetTag(Resource.Id.productSummaryImageView, _productIimageView);
                view.SetTag(Resource.Id.productCountTextView, _productCountTextView);
                view.SetTag(Resource.Id.productNameTextView, _productNameTextView);
                view.SetTag(Resource.Id.productSummaryRelativeLayout, _productSummaryRelativeLayout);
            }
            else
            {
                _favoritsImageView = (ImageView)view.GetTag(Resource.Id.addToFavoritsImageView);
                _productCountTextView = (TextView)view.GetTag(Resource.Id.productCountTextView);
                _productNameTextView = (TextView)view.GetTag(Resource.Id.productNameTextView);
                _productRemoveButton = (Button)view.GetTag(Resource.Id.removeProductButton);
                _productAddButton = (Button)view.GetTag(Resource.Id.addProductButton);
                _productIimageView = (ImageView)view.GetTag(Resource.Id.productSummaryImageView);
                _productPriceTextView = (TextView)view.GetTag(Resource.Id.productPriceTextView);
                _productSummaryRelativeLayout = (RelativeLayout)view.GetTag(Resource.Id.productSummaryRelativeLayout);
            }

            if (!String.IsNullOrEmpty(item.image))
                UrlImageViewHelper.SetUrlDrawable(_productIimageView, item.image, Resource.Drawable.igmart, UrlImageViewHelper.CacheDurationFiveDays);
            else
                _productIimageView.SetImageResource(Resource.Drawable.igmart);


            if (item.IsFavorit)
                _favoritsImageView.SetImageResource(Resource.Drawable.Favorits);
            else
                _favoritsImageView.SetImageResource(Resource.Drawable.FavoritsGray);

            _productCountTextView.Text = item.count.ToString();
            _productNameTextView.Text = item.product_name;
            _productPriceTextView.Text = string.Format(" {0} تومان", item.price);





            var _favoritsTouchlistener = new OnTouchListener();
            _favoritsImageView.SetOnTouchListener(_favoritsTouchlistener);
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
                    if (await ProductManager.AddToFavorits(this[position]) == true)
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
                    NotifyDataSetChanged();
                    OnDataChanged();
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
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
                        OnShoppingCardItemRemoved(item);
                    }
                    NotifyDataSetChanged();
                    OnDataChanged();
                    AnatoliAndroid.Activities.AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                }
            };

            OnTouchListener listener = new OnTouchListener();
            _productSummaryRelativeLayout.SetOnTouchListener(listener);
            listener.Swipe += (s, e) =>
            {
                Console.WriteLine("swipe on product: " + item.product_name);
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

        class OnTouchListener : Java.Lang.Object, View.IOnTouchListener
        {
            long _downTime;
            float _downX;
            float _downY;
            long _upTime;
            public bool OnTouch(View v, MotionEvent e)
            {
                switch (e.Action)
                {
                    case MotionEventActions.Up:
                        _upTime = Now();
                        if ((_upTime - _downTime) > 1000)
                            OnLongClick();
                        else if ((_upTime - _downTime) > 100)
                        {
                            Console.WriteLine("x= " + (e.GetX() - _downX).ToString() + " y= " + (e.GetY() - _downY).ToString());
                            if (e.GetX() - _downX < -200)
                            {
                                if (Math.Abs(e.GetY() - _downY) < 100)
                                {
                                    OnSwipe();
                                }
                            }
                        }
                        else
                            OnClick();
                        break;
                    case MotionEventActions.Down:
                        _downTime = Now();
                        _downX = e.GetX();
                        _downY = e.GetY();
                        break;
                    default:
                        break;
                }
                return true;
            }

            void OnClick()
            {
                if (Click != null)
                {
                    Click.Invoke(this, new EventArgs());
                }
            }
            public event EventHandler Click;

            void OnLongClick()
            {
                if (LongClick != null)
                {
                    LongClick.Invoke(this, new EventArgs());
                }
            }
            public event EventHandler LongClick;

            void OnSwipe()
            {
                if (Swipe != null)
                {
                    Swipe.Invoke(this, new EventArgs());
                }
            }
            public event EventHandler Swipe;

            long Now()
            {
                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                return milliseconds;
            }
        }


    }
}