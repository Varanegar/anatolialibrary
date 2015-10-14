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
using Anatoli.Framework.Model;
using Anatoli.Framework.DataAdapter;
using AnatoliAndroid.ListAdapters;
using Anatoli.Framework.AnatoliBase;

namespace AnatoliAndroid.Fragments
{
    abstract class BaseListFragment<BaseDataManager, DataListAdapter, DataModel> : Fragment
        where BaseDataManager : BaseManager<BaseDataAdapter<DataModel>, DataModel>, new()
        where DataListAdapter : BaseListAdapter<DataModel>, new()
        where DataModel : BaseDataModel, new()
    {
        protected View _view;
        protected ListView _listView;
        protected DataListAdapter _listAdapter;
        protected object[] queryParams;
        protected BaseDataManager _dataManager;
        private bool _firstShow = true;
        public BaseListFragment()
            : base()
        {
            _listAdapter = new DataListAdapter();
            _dataManager = new BaseDataManager();
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            _view = inflater.Inflate(
                Resource.Layout.ItemsListLayout, container, false);
            _listView = _view.FindViewById<ListView>(Resource.Id.itemsListView);
            _listView.ScrollStateChanged += _listView_ScrollStateChanged;
            _listView.Adapter = _listAdapter;
            return _view;
        }
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (_firstShow)
            {
                SetParameters();
                _listAdapter.List = await _dataManager.GetNextAsync();
                _listAdapter.NotifyDataSetChanged();
            }
            _firstShow = false;
        }

        async void _listView_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            if (e.ScrollState == ScrollState.Idle)
            {
                if ((_listView.Adapter.Count - 1) <= _listView.LastVisiblePosition)
                {
                    var list = await _dataManager.GetNextAsync();
                    _listAdapter.List.AddRange(list);
                    _listAdapter.NotifyDataSetChanged();
                }
            }
        }
        protected void SetParameters()
        {
            var parameters = CreateQueryParameters();
            _dataManager.SetQueryParameters(parameters);
        }
        protected abstract List<Query.QueryParameter> CreateQueryParameters();
    }
}