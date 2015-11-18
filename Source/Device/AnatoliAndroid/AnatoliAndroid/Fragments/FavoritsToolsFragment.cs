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
    public class FavoritsDialogTools : ListToolsDialog
    {
        View _view;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _view = inflater.Inflate(Resource.Layout.FavoritsToolsLayout, container, false);

            var shareButton = _view.FindViewById<Button>(Resource.Id.shareFavortisButton);
            shareButton.UpdateWidth();
            var removeAllButton = _view.FindViewById<Button>(Resource.Id.removeAllFavoritsButton);
            removeAllButton.UpdateWidth();
            removeAllButton.Click += async (s, e) =>
                {
                    var result = await ProductManager.RemoveFavoritsAll();
                    if (result)
                    {
                        Dismiss();
                        OnFavoritsRemoved();
                    }
                };
            return _view;
        }

        private void OnFavoritsRemoved()
        {
            if (FavoritsRemoved != null)
            {
                FavoritsRemoved.Invoke();
            }
        }
        public event FavoritsRemovedEventHandler FavoritsRemoved;
        public delegate void FavoritsRemovedEventHandler();
    }
}