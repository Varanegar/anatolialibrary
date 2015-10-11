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
using Anatoli.App.Adapter;
using Anatoli.Framework.Model;
using Anatoli.App.Manager;

namespace AnatoliAndroid.Fragments
{
    class StoresListFragment : BaseListFragment<StoreAdapter, BaseListModel<StoreDataModel>, StoreDataModel>
    {
        public StoresListFragment() : base(new StoreManager()) { }
    }
}