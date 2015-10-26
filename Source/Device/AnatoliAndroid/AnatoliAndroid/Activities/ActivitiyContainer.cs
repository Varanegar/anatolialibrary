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
    class ActivityContainer
    {
        private static ActivityContainer instance;
        private Activity _activity;
        Android.App.Fragment _currentFragment;
        public Android.App.Fragment GetCurrentFragment()
        {
            return _currentFragment;
        }
        public Activity Activity { get { return _activity; } }
        public static ActivityContainer GetInstance()
        {
            if (instance == null)
                throw new NullReferenceException();
            return instance;
        }
        public static void Initialize(Activity activity)
        {
            if (instance == null)
                instance = new ActivityContainer(activity);
        }
        private ActivityContainer()
        {

        }

        private ActivityContainer(Activity activity)
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
            _currentFragment = fragment;
            return fragment;
        }
    }
}