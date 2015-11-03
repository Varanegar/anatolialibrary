using System;
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
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("پیغام ها")]
    class MessagesListFragment : BaseListFragment<MessageManager, MessageListAdapter, MessageModel>
    {
        public MessagesListFragment()
        {
            _listAdapter.MessageDeleted += (item) =>
                {
                    _listAdapter.List.Remove(item);
                    _listAdapter.NotifyDataSetChanged();
                    _listView.InvalidateViews();
                };
        }
        protected override List<Anatoli.Framework.AnatoliBase.QueryParameter> CreateQueryParameters()
        {
            return new List<QueryParameter>();
        }

        protected override string GetTableName()
        {
            return "messages_view";
        }

        protected override string GetWebServiceUri()
        {
            return "None";
        }
    }
}