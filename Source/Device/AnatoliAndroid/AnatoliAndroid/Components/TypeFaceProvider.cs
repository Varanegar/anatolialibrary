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
using Android.Graphics;

namespace AnatoliAndroid.Components
{
    class TypeFaceProvider
    {
        public Typeface _typeface = null;
        private static TypeFaceProvider _instance;
        public static Typeface GetTypeFace(Context context, String asset)
        {
            if (_instance == null)
            {
                _instance = new TypeFaceProvider(context, asset);
            }
            return _instance._typeface;
        }
        private TypeFaceProvider(Context context, String asset)
        {
            _typeface = Android.Graphics.Typeface.CreateFromAsset(context.Assets, asset);
        }
    }
}