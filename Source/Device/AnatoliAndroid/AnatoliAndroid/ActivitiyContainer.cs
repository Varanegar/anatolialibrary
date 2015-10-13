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

namespace AnatoliAndroid
{
    class ActivityContainer
    {
        private static ActivityContainer instance;
        private Activity _activity;
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
    }
}