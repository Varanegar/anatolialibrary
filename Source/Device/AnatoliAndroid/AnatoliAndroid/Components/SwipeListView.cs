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
using Android.Views.Animations;
using AnatoliAndroid.Activities;
using Android.Util;

namespace AnatoliAndroid.Components
{
    class SwipeListView : ListView
    {
        public SwipeListView(Context context)
            : base(context)
        {
            ScrollStateChanged += HideVisibleOptions;
        }
        public SwipeListView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            ScrollStateChanged += HideVisibleOptions;
        }

        public SwipeListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            ScrollStateChanged += HideVisibleOptions;
        }
        public SwipeListView(IntPtr a, JniHandleOwnership b)
            : base(a, b)
        {
            ScrollStateChanged += HideVisibleOptions;
        }
        public void HideVisibleOptions(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            try
            {
                //var sl = _listView as SwipeListView;
                //sl.CloseOpenedItems();
                for (int i = FirstVisiblePosition; i < LastVisiblePosition; i++)
                {
                    try
                    {
                        HideOptions(i);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public void ShowOptions(int position)
        {
            try
            {
                int firstPosition = FirstVisiblePosition;
                int childPosition = position - firstPosition;
                var view = GetChildAt(childPosition);
                var optionsLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.optionslinearLayout);
                if (optionsLinearLayout.Visibility == ViewStates.Gone)
                {
                    optionsLinearLayout.Visibility = ViewStates.Visible;
                    var anim = AnimationUtils.LoadAnimation(AnatoliApp.GetInstance().Activity, Resource.Animation.slideIn);
                    optionsLinearLayout.StartAnimation(anim);
                }
            }
            catch (Exception)
            {

            }
        }
        public void HideOptions(int position)
        {
            try
            {
                int firstPosition = FirstVisiblePosition;
                int childPosition = position - firstPosition;
                var view = GetChildAt(childPosition);
                var optionsLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.optionslinearLayout);
                if (optionsLinearLayout.Visibility == ViewStates.Visible)
                {
                    var anim = AnimationUtils.LoadAnimation(AnatoliApp.GetInstance().Activity, Resource.Animation.slideOut);
                    optionsLinearLayout.StartAnimation(anim);
                    optionsLinearLayout.Visibility = ViewStates.Gone;
                }

            }
            catch (Exception)
            {

            }
        }

    }
}