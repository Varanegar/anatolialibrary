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
using System.Threading.Tasks;
using AnatoliAndroid.Activities;
using FortySevenDeg.SwipeListView;

namespace AnatoliAndroid.Fragments
{
    abstract class BaseListFragment<BaseDataManager, DataListAdapter, ListTools, DataModel> : Fragment
        where BaseDataManager : BaseManager<BaseDataAdapter<DataModel>, DataModel>, new()
        where DataListAdapter : BaseListAdapter<BaseDataManager, DataModel>, new()
        where ListTools : ListToolsDialog, new()
        where DataModel : BaseDataModel, new()
    {
        protected View _view;
        protected ListView _listView;
        protected DataListAdapter _listAdapter;
        protected BaseDataManager _dataManager;
        protected ListTools _toolsDialogFragment;
        private bool _firstShow = true;
        protected List<Tuple<string, string>> _searchKeyWords;
        TextView _resultTextView;
        public BaseListFragment()
            : base()
        {
            _listAdapter = new DataListAdapter();
            _dataManager = new BaseDataManager();
            _toolsDialogFragment = new ListTools();
        }
        public async Task Search(params Tuple<string, string>[] keywords)
        {
            _searchKeyWords = new List<Tuple<string, string>>();
            foreach (var item in keywords)
            {
                _searchKeyWords.Add(item);
            }
            SetParameters();
            try
            {
                _listAdapter.List = await _dataManager.GetNextAsync();
                _listAdapter.NotifyDataSetChanged();
                if (_listAdapter.List.Count == 0)
                    OnEmptyList();
                else
                    OnFullList();
            }
            catch (Exception)
            {

            }
        }
        public void ClearSearch()
        {
            _searchKeyWords = null;
        }
        protected virtual View InflateLayout(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(
                Resource.Layout.ItemsListLayout, container, false);
            return view;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = InflateLayout(inflater, container, savedInstanceState);
            _listView = _view.FindViewById<ListView>(Resource.Id.itemsListView);
            _resultTextView = _view.FindViewById<TextView>(Resource.Id.resultTextView);
            _listView.ScrollStateChanged += _listView_ScrollStateChanged;
            _listView.Adapter = _listAdapter;

            if (_toolsDialogFragment.GetType() == typeof(NoListToolsDialog))
            {
                AnatoliApp.GetInstance().HideMenuIcon();
            }
            else
            {
                AnatoliApp.GetInstance().ShowMenuIcon();
                AnatoliApp.GetInstance().MenuClicked = () =>
                {
                    _toolsDialogFragment.Show(AnatoliApp.GetInstance().Activity.FragmentManager, "sss");
                };
            }
            EmptyList += (s, e) =>
            {
                _resultTextView.Visibility = ViewStates.Visible;
                _resultTextView.Text = "هیچ موردی یافت نشد";
            };
            FullList += (s, e) =>
            {
                _resultTextView.Visibility = ViewStates.Gone;
            };
            return _view;
        }
        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (_firstShow)
            {
                SetParameters();
                try
                {
                    _listAdapter.List = await _dataManager.GetNextAsync();
                    if (_listAdapter.Count == 0)
                        OnEmptyList();
                    else
                        OnFullList();
                    _listAdapter.NotifyDataSetChanged();
                }
                catch (Exception)
                {

                }
            }
            _firstShow = false;
        }

        async void _listView_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            if (e.ScrollState == ScrollState.Idle)
            {
                if ((_listView.Adapter.Count - 1) <= _listView.LastVisiblePosition)
                {
                    try
                    {
                        var list = await _dataManager.GetNextAsync();
                        _listAdapter.List.AddRange(list);
                        _listAdapter.NotifyDataSetChanged();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        protected void SetParameters()
        {
            var parameters = CreateQueryParameters();
            if (_searchKeyWords != null)
            {
                parameters.Clear();
                foreach (var item in _searchKeyWords)
                {
                    var p = new SearchFilterParam(item.Item1, item.Item2);
                    parameters.Add(p);
                }
            }
            _dataManager.SetQueries(new SelectQuery(GetTableName(), "Or", parameters), null);
        }

        protected abstract List<QueryParameter> CreateQueryParameters();
        protected abstract string GetTableName();
        protected abstract string GetWebServiceUri();

        public void HideTools()
        {
            _toolsDialogFragment.Dismiss();
        }
        public void ShowTools()
        {
            var transaction = FragmentManager.BeginTransaction();
            _toolsDialogFragment.Show(transaction, "tools_dialog");
        }
        void OnEmptyList()
        {
            if (EmptyList != null)
            {
                EmptyList.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler EmptyList;

        void OnFullList()
        {
            if (FullList != null)
            {
                FullList.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler FullList;
    }
}