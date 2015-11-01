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
using AnatoliAndroid.Fragments;

namespace AnatoliAndroid
{
    public static class Extentions
    {
        public static string GetTitle(this Fragment fragment)
        {
            var title = Attribute.GetCustomAttribute(fragment.GetType(), typeof(FragmentTitle));
            if (title != null)
                return (title as FragmentTitle).Title;
            else
                return "";
        }
    }
}