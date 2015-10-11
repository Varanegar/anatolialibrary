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

namespace AnatoliAndroid.Fragments
{
    abstract class BaseListFragment<DataAdapter, DataList, Data> : Fragment 
        where DataAdapter : BaseDataAdapter<DataList, Data>, new()
        where DataList : BaseListModel<Data>, new()
        where Data : BaseDataModel, new()
    {
        protected BaseManager<DataAdapter, DataList, Data> _manager;
        protected View _view;
        protected ListView _listView;
        public BaseListFragment(BaseManager<DataAdapter, DataList, Data> manager)
            : base()
        {
            _manager = manager;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            _view = inflater.Inflate(
                Resource.Layout.ItemsListLayout, container, false);
            _listView = _view.FindViewById<ListView>(Resource.Id.itemsListView);
            // todo : set base list adapter here
            return _view;
        }
        

        public async override void OnResume()
        {
            base.OnResume();
            var list = await _manager.GetAllAsync();
        }

    }
}