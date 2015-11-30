﻿using System;
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
using Anatoli.App.Model.Store;
using Anatoli.App.Manager;
using AnatoliAndroid.Activities;
using System.Threading.Tasks;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("سفارش ها")]
    public class OrderDetailFragment : Fragment
    {
        ListView _itemsListView;
        TextView _dateTextView;
        TextView _storeNameTextView;
        TextView _priceTextView;
        TextView _orderIdTextView;
        TextView _orderStatusTextView;
        Button _addAllButton;
        string _orderId;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.OrderViewLayout, null);
             _dateTextView = view.FindViewById<TextView>(Resource.Id.dateTextView);
             _storeNameTextView = view.FindViewById<TextView>(Resource.Id.storeNameTextView);
             _priceTextView = view.FindViewById<TextView>(Resource.Id.priceTextView);
             _orderIdTextView = view.FindViewById<TextView>(Resource.Id.orderNoTextView);
             _orderStatusTextView = view.FindViewById<TextView>(Resource.Id.orderStatusTextView);
            _itemsListView = view.FindViewById<ListView>(Resource.Id.itemsListView);
            _addAllButton = view.FindViewById<Button>(Resource.Id.addAllButton);
            _addAllButton.UpdateWidth();
            _orderId = Arguments.GetString("order_id");
            return view;
        }
        public async override void OnStart()
        {
            base.OnStart();

            OrderModel order = await OrderManager.GetOrderAsync(_orderId);
            _dateTextView.Text = order.order_date;
            _storeNameTextView.Text = order.store_name;
            _priceTextView.Text = order.order_price.ToString();
            _orderIdTextView.Text = order.order_id;
            _orderStatusTextView.Text = order.order_status.ToString();


            List<OrderItemModel> items = await OrderItemsManager.GetItemsAsync(_orderId);
            OrderDetailAdapter adapter = new OrderDetailAdapter(items, AnatoliApp.GetInstance().Activity);
            adapter.DataChanged += (s, e) =>
            {
                _itemsListView.InvalidateViews();
            };
            _itemsListView.Adapter = adapter;

            _addAllButton.Click += async (s, e) =>
            {
                int a = 0;
                foreach (var item in items)
                {
                    await Task.Delay(100);
                    if (await ShoppingCardManager.AddProductAsync(item.product_id, item.item_count))
                    {
                        a += item.item_count;
                    }

                }
                AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                Toast.MakeText(AnatoliApp.GetInstance().Activity,String.Format("{0} آیتم به سبد خرید اضافه شد",a.ToString()), ToastLength.Short).Show();
            };
        }

        class OrderDetailAdapter : BaseAdapter<OrderItemModel>
        {
            List<OrderItemModel> items;
            Activity _context;
            public OrderDetailAdapter(List<OrderItemModel> items, Activity context)
            {
                this.items = items;
                _context = context;
            }
            public override OrderItemModel this[int position]
            {
                get
                {
                    return items[position];
                }
            }

            public override int Count
            {
                get
                {
                    return items.Count;
                }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.OrderItemModelLayout, null);
                OrderItemModel item = null;
                if (items != null)
                    item = items[position];
                else
                    return convertView;
                TextView productCountTextView = convertView.FindViewById<TextView>(Resource.Id.productCountTextView);
                TextView productPriceTextView = convertView.FindViewById<TextView>(Resource.Id.productPriceTextView);
                TextView productNameTextView = convertView.FindViewById<TextView>(Resource.Id.productNameTextView);
                ImageView addProductImageView = convertView.FindViewById<ImageView>(Resource.Id.addProductImageView);
                ImageView addToFavoritsImageView = convertView.FindViewById<ImageView>(Resource.Id.addToFavoritsImageView);
                if (item.IsFavorit)
                    addToFavoritsImageView.SetImageResource(Resource.Drawable.Favorits);
                else
                    addToFavoritsImageView.SetImageResource(Resource.Drawable.FavoritsGray);
                productPriceTextView.Text = " ("+item.item_price.ToString()+" تومان) ";
                productCountTextView.Text = item.item_count.ToString();
                productNameTextView.Text = item.product_name;
                addProductImageView.Click += async (s,e) =>
                {
                    await ShoppingCardManager.AddProductAsync(item.product_id, item.item_count);
                    AnatoliApp.GetInstance().ShoppingCardItemCount.Text = (await ShoppingCardManager.GetItemsCountAsync()).ToString();
                    Toast.MakeText(_context, "به سبد خرید اضافه شد", ToastLength.Short).Show();
                };
                addToFavoritsImageView.Click += async (s, e) =>
                {
                    if (item.IsFavorit)
                    {
                        if (await ProductManager.RemoveFavorit(this[position].product_id) == true)
                        {
                            this[position].favorit = 0;
                            NotifyDataSetChanged();
                            OnDataChanged();
                        }
                    }
                    else
                    {
                        if (await ProductManager.AddToFavorits(this[position].product_id) == true)
                        {
                            this[position].favorit = 1;
                            NotifyDataSetChanged();
                            OnDataChanged();
                        }
                    }
                };
                return convertView;
            }

            void OnDataChanged()
            {
                if (DataChanged != null)
                {
                    DataChanged.Invoke(this, new EventArgs());
                }
            }

            public event EventHandler DataChanged;
        }
    }
}