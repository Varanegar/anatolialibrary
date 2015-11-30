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
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    class OrdersListFragment : BaseListFragment<OrderManager, OrdersListAdapter, NoListToolsDialog, OrderModel>
    {
        protected override List<Anatoli.Framework.AnatoliBase.QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            return parameters;
        }

        protected override string GetTableName()
        {
            return "orders_view";
        }

        protected override string GetWebServiceUri()
        {
            return "none";
        }
        public override void OnResume()
        {
            base.OnResume();
            EmptyList += (s, e) =>
            {
                if (_listAdapter.List.Count == 0)
                {
                    Toast.MakeText(AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity, "هیچ سفارشی ثبت نشده است", ToastLength.Short).Show();
                }
            };
        }
    }
}