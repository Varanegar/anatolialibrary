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
    class MessageListAdapter : BaseListAdapter<MessageManager, MessageModel>
    {
        public override View GetItemView(int position, View convertView, ViewGroup parent)
        {
            convertView = _context.LayoutInflater.Inflate(Resource.Layout.MessageItemLayout, null);
            MessageModel item = null;
            if (List != null)
                item = List[position];
            else
                return convertView;
            TextView contentTextView = convertView.FindViewById<TextView>(Resource.Id.contentTextView);
            TextView storeNameTextView = convertView.FindViewById<TextView>(Resource.Id.storeNameTextView);
            TextView dateTextView = convertView.FindViewById<TextView>(Resource.Id.dateTextView);
            TextView timeTextView = convertView.FindViewById<TextView>(Resource.Id.timeTextView);
            contentTextView.Text = item.content;
            storeNameTextView.Text = item.store_name;
            dateTextView.Text = item.date;
            timeTextView.Text = item.time;
            return convertView;
        }
    }
}