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

namespace AnatoliAndroid.Activities
{
    class AnatoliApp
    {
        private static AnatoliApp instance;
        private Activity _activity;

        private static Stack<Tuple<string, Fragment>> _stack;
        public Android.App.Fragment GetCurrentFragment()
        {
            try
            {
                return _stack.Peek().Item2;
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
            _stack = new Stack<Tuple<string, Fragment>>();
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
            _stack.Push(new Tuple<string, Fragment>(tag, fragment));
            return fragment;
        }
        public bool BackFragment()
        {
            var transaction = _activity.FragmentManager.BeginTransaction();
            try
            {
                var stackItem = _stack.Pop();
                stackItem = _stack.Peek();
                transaction.Replace(Resource.Id.content_frame, stackItem.Item2, stackItem.Item1);
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}