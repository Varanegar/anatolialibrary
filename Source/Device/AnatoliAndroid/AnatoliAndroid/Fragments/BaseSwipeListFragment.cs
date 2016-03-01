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
using Anatoli.Framework.Manager;
using Anatoli.Framework.DataAdapter;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.Model;
using System.Threading.Tasks;
using AnatoliAndroid.Activities;
using AnatoliAndroid.Components;
using Android.Views.Animations;
using Anatoli.Framework;

namespace AnatoliAndroid.Fragments
{
    abstract class BaseSwipeListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel> : BaseListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel>
        where BaseDataManager : BaseManager<DataModel>, new()
        where DataListAdapter : BaseSwipeListAdapter<BaseDataManager, DataModel>, new()
        where ListTools : ListToolsDialog, new()
        where DataModel : BaseViewModel, new()
    {
        protected override View InflateLayout(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(
                Resource.Layout.ItemsSwipeListLayout, container, false);
            return view;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _listAdapter.SwipeRight += (s, p) => { _listView.HideOptions(p); };
            _listAdapter.SwipeLeft += (s, p) => { _listView.ShowOptions(p); };
            _listAdapter.OptionsClick += async (s, p) =>
            {
                await Task.Delay(1000);
                try
                {
                    _listView.HideOptions(p);
                }
                catch (Exception ex)
                {
                    ex.SendTrace();
                }
            };
            return view;
        }



    }



}