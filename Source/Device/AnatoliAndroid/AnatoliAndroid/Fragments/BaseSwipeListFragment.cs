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
using FortySevenDeg.SwipeListView;
using System.Threading.Tasks;

namespace AnatoliAndroid.Fragments
{
    abstract class BaseSwipeListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel> : BaseListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel>
        where BaseDataManager : BaseManager<BaseDataAdapter<DataModel>, DataModel>, new()
        where DataListAdapter : BaseSwipeListAdapter<BaseDataManager, DataModel>, new()
        where ListTools : ListToolsDialog, new()
        where DataModel : BaseDataModel, new()
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

            _listAdapter.BackClick += async (s, p) =>
            {
                await Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    (_listView as SwipeListView).CloseAnimate(p);
                });
            };
            _listView.ScrollStateChanged += _listView_ScrollStateChanged;
            return view;
        }

        void _listView_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            var sl = _listView as SwipeListView;
            try
            {
                sl.CloseOpenedItems();
            }
            catch (Exception)
            {

            }
        }
    }
}