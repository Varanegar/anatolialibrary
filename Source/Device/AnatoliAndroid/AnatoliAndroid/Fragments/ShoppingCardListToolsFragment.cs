using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Anatoli.App.Manager;

namespace AnatoliAndroid.Fragments
{
    public class ShoppingCardListToolsFragment : ListToolsDialog
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.ShoppingCardToolsLayout, container, false);

            var shareButton = view.FindViewById<Button>(Resource.Id.shareButton);
            shareButton.UpdateWidth();
            var removeAllButton = view.FindViewById<Button>(Resource.Id.removeAllButton);
            removeAllButton.UpdateWidth();
            removeAllButton.Click += async (s, e) =>
            {
                var result = await ShoppingCardManager.ClearAsync();
                if (result)
                {
                    OnShoppingCardCleared();
                    Dismiss();
                }
            };
            return view;
        }

        void OnShoppingCardCleared()
        {
            if (ShoppingCardCleared != null)
                ShoppingCardCleared.Invoke();
        }

        public event ShoppingCardClearedEventHandler ShoppingCardCleared;
        public delegate void ShoppingCardClearedEventHandler();
    }
}