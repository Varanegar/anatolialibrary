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

namespace AnatoliAndroid.Fragments
{
    class FragmentTitle : Attribute
    {
        public string Title { get; set; }
        public FragmentTitle(string title)
        {
            Title = title;
        }
    }
}