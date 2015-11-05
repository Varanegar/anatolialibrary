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
using Android.Util;

namespace AnatoliAndroid.Components
{
    class AnatoliButton : Button
    {
        private const string Tag = "Button";

        protected AnatoliButton(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public AnatoliButton(Context context)
            : this(context, null)
        {
        }

        public AnatoliButton(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public AnatoliButton(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            var a = context.ObtainStyledAttributes(attrs,
                    Resource.Styleable.CustomFonts);
            var customFont = a.GetString(Resource.Styleable.CustomFonts_customFont);
            SetCustomFont(customFont);
            a.Recycle();
        }

        public void SetCustomFont(string asset)
        {
            try
            {
                SetTypeface(Android.Graphics.Typeface.CreateFromAsset(Context.Assets, asset), Android.Graphics.TypefaceStyle.Normal);
            }
            catch (Exception e)
            {
                Log.Error(Tag, string.Format("Could not get Typeface: {0} Error: {1}", asset, e));
                return;
            }
        }
    }
}