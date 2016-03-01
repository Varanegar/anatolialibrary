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
using AnatoliAndroid.Components;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.ListAdapters
{
    abstract class BaseSwipeListAdapter<BaseDataManager, DataModel> : BaseListAdapter<BaseDataManager, DataModel>
        where DataModel : BaseViewModel, new()
        where BaseDataManager : BaseManager<DataModel>, new()
    {
        public void OnSwipeLeft(int position)
        {
            if (SwipeLeft != null)
            {
                SwipeLeft.Invoke(this, position);
            }
        }
        public event SwipeLeftEventHandler SwipeLeft;
        public delegate void SwipeLeftEventHandler(object sender, int position);
        public void OnSwipeRight(int position)
        {
            if (SwipeRight != null)
            {
                SwipeRight.Invoke(this, position);
            }
        }
        public event SwipeRightEventHandler SwipeRight;
        public delegate void SwipeRightEventHandler(object sender, int position);
        protected virtual void OptionsClicked(int position)
        {
            if (OptionsClick != null)
            {
                OptionsClick(this, position);
            }
        }
        public event OptionsClickedEventHandler OptionsClick;
        public delegate void OptionsClickedEventHandler(object sender, int position);
    }
}