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
using AnatoliAndroid.Components;
using AnatoliAndroid.Activities;
using System.Threading.Tasks;
using Android.Graphics;

namespace AnatoliAndroid.Fragments
{
    [FragmentTitle("")]
    public class FirstFragment : Fragment
    {
        ImageView _slideShowImageView;
        AnatoliSlideShow _slideShow;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.FirstLayout, container, false);
            _slideShowImageView = view.FindViewById<ImageView>(Resource.Id.slideShowImageView);
            var progress = view.FindViewById<ProgressBar>(Resource.Id.progress);
            _slideShow = new AnatoliSlideShow(_slideShowImageView, progress);
            //var tl = new OnTouchListener();
            //_slideShowImageView.SetOnTouchListener(tl);
            //tl.SwipeRight += (s, e) =>
            //{
            //    _slideShow.Stop();
            //};

            AnatoliAndroid.Components.AnatoliSlideShow.OnClick click1 = new AnatoliAndroid.Components.AnatoliSlideShow.OnClick(() => { Toast.MakeText(AnatoliApp.GetInstance().Activity, "Item 1 selected", ToastLength.Short).Show(); });
            var c1 = new Tuple<BitmapContainer, AnatoliAndroid.Components.AnatoliSlideShow.OnClick>(new BitmapContainer("http://www.psdgraphics.com/wp-content/uploads/2015/06/mosaic-background.png", null), click1);
            _slideShow.Source.Add(c1);
            AnatoliAndroid.Components.AnatoliSlideShow.OnClick click2 = new AnatoliAndroid.Components.AnatoliSlideShow.OnClick(() => { Toast.MakeText(AnatoliApp.GetInstance().Activity, "Item 2 selected", ToastLength.Short).Show(); });
            var c2 = new Tuple<BitmapContainer, AnatoliAndroid.Components.AnatoliSlideShow.OnClick>(new BitmapContainer("http://www.ayuz.ir/files/photos/s1.png", null), click2);
            _slideShow.Source.Add(c2);
            AnatoliAndroid.Components.AnatoliSlideShow.OnClick click3 = new AnatoliAndroid.Components.AnatoliSlideShow.OnClick(() => { Toast.MakeText(AnatoliApp.GetInstance().Activity, "Item 3 selected", ToastLength.Short).Show(); });
            var c3 = new Tuple<BitmapContainer, AnatoliAndroid.Components.AnatoliSlideShow.OnClick>(new BitmapContainer("https://pixabay.com/static/uploads/photo/2012/11/06/03/47/background-64259_960_720.jpg", null), click3);
            _slideShow.Source.Add(c3);




            return view;
        }
        public override async void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().HideSearchIcon();
            AnatoliApp.GetInstance().HideMenuIcon();
            await System.Threading.Tasks.Task.Run(() => { _slideShow.Start(); });
        }


    }


}