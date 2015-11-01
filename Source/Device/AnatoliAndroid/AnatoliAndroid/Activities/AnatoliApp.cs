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
using AnatoliAndroid;

namespace AnatoliAndroid.Activities
{
    class AnatoliApp
    {
        private static AnatoliApp instance;
        private Activity _activity;
        public TextView ToolbarTextView { get; set; }
        public Android.Locations.LocationManager LocationManager;

        private static Stack<StackItem> _stack;
        public Type GetCurrentFragmentType()
        {
            try
            {
                return _stack.Peek().FragmentType;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Activity Activity { get { return _activity; } }
        public static AnatoliApp GetInstance()
        {
            if (instance == null)
                throw new NullReferenceException();
            return instance;
        }
        public static void Initialize(Activity activity)
        {
            instance = new AnatoliApp(activity);
            _stack = new Stack<StackItem>();
        }
        private AnatoliApp()
        {

        }

        private AnatoliApp(Activity activity)
        {
            _activity = activity;
        }
        public FragmentType SetFragment<FragmentType>(FragmentType fragment, string tag) where FragmentType : Android.App.Fragment, new()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            if (fragment == null)
            {
                fragment = new FragmentType();
            }
            transaction.Replace(Resource.Id.content_frame, fragment, tag);
            transaction.Commit();
            ToolbarTextView.Text = fragment.GetTitle();
            _stack.Push(new StackItem(tag, fragment.GetType()));
            return fragment;
        }
        public bool BackFragment()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            try
            {
                var stackItem = _stack.Pop();
                stackItem = _stack.Peek();
                var fragment = Activator.CreateInstance(stackItem.FragmentType);
                transaction.Replace(Resource.Id.content_frame, fragment as Fragment, stackItem.FragmentName);
                transaction.Commit();
                ToolbarTextView.Text = (fragment as Fragment).GetTitle();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public class StackItem
        {
            public string FragmentName;
            public Type FragmentType;
            public StackItem(string FragmentName, Type FragmentType)
            {
                this.FragmentName = FragmentName;
                this.FragmentType = FragmentType;
            }
        }

    }
}