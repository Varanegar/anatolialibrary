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
using Anatoli.Framework.Manager;
using Anatoli.Framework.DataAdapter;

namespace AnatoliAndroid.ListAdapters
{
    class BaseSwipeListAdapter<BaseDataManager, DataModel> : BaseListAdapter<BaseDataManager, DataModel>
        where DataModel : BaseDataModel, new()
        where BaseDataManager : BaseManager<BaseDataAdapter<DataModel>, DataModel>, new()
    {
        //protected virtual void OnBackClicked(int position)
        //{
        //    if (BackClick != null)
        //    {
        //        BackClick(this,position);
        //    }
        //}
        //public event BackClickedEventHandler BackClick;
        //public delegate void BackClickedEventHandler(object sender,int position);
    }
}