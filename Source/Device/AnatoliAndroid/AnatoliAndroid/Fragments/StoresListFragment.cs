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
using Anatoli.Framework.Model;
using Anatoli.App.Manager;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;
using Android.Locations;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("انتخاب فروشگاه")]
    class StoresListFragment : BaseListFragment<StoreManager, StoresListAdapter,NoListToolsDialog, StoreDataModel>
    {

        public StoresListFragment()
        {
            _listAdapter.StoreSelected += (store) =>
                {
                    foreach (var item in _listAdapter.List)
                    {
                        if (item != store)
                            item.selected = 0;
                        else
                            item.selected = 1;
                    }
                    _listAdapter.NotifyDataSetChanged();
                };
        }


        protected override List<QueryParameter> CreateQueryParameters()
        {
            var parameters = new List<QueryParameter>();
            return parameters;
        }

        protected override string GetTableName()
        {
            return "stores";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Stores.StoresView;
        }

    }
}