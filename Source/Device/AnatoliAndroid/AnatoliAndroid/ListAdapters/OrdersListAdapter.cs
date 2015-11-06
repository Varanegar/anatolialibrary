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
using Anatoli.App.Manager;
using Anatoli.App.Model.Store;

namespace AnatoliAndroid.ListAdapters
{
    class OrdersListAdapter : BaseListAdapter<OrderManager, OrderModel>
    {
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.OrderItemLayout, null);
            OrderModel item = null;
            if (List != null)
                item = List[position];
            else
                return convertView;
            TextView dateTextView = convertView.FindViewById<TextView>(Resource.Id.dateTextView);
            TextView storeNameTextView = convertView.FindViewById<TextView>(Resource.Id.storeNameTextView);
            TextView priceTextView = convertView.FindViewById<TextView>(Resource.Id.priceTextView);
            TextView orderIdTextView = convertView.FindViewById<TextView>(Resource.Id.orderNoTextView);
            TextView orderStatusTextView = convertView.FindViewById<TextView>(Resource.Id.orderStatusTextView);
            dateTextView.Text = item.order_date;
            storeNameTextView.Text = item.store_name;
            priceTextView.Text = item.order_price.ToString();
            orderIdTextView.Text = item.order_id;
            orderStatusTextView.Text = item.order_status.ToString();
            return convertView;
        }
    }
}