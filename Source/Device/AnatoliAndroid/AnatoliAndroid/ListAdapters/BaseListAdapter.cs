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
using Anatoli.Framework.Model;

namespace AnatoliAndroid.ListAdapters
{
    class BaseListAdapter<DataModel> : BaseAdapter<DataModel>
        where DataModel : BaseDataModel, new()
    {
        protected List<DataModel> _list;
        protected Activity _context;
        public BaseListAdapter()
        {
            _list = new List<DataModel>();
            _context = ActivityContainer.GetInstance().Activity;
        }

        public override int Count
        {
            get { return (_list == null) ? 0 : _list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override DataModel this[int position]
        {
            get { return _list[position]; }
        }
        public void SetList(List<DataModel> list)
        {
            _list = list;
            NotifyDataSetChanged();
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            return GetItemView(position, convertView, parent);
        }
        public virtual View GetItemView(int position, View convertView, ViewGroup parent)
        {
            return convertView;
        }
    }
}