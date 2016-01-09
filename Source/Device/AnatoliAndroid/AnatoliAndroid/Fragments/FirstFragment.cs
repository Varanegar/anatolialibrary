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
        ImageView _slideShow;
        int _imageIndex = 0;
        List<Tuple<BitmapContainer, OnClick>> _imageList = new List<Tuple<BitmapContainer, OnClick>>();
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
            _slideShow = view.FindViewById<ImageView>(Resource.Id.slideShowImageView);

            OnClick click1 = new OnClick(() => { Toast.MakeText(AnatoliApp.GetInstance().Activity, "Item 1 selected", ToastLength.Short).Show(); });
            var c1 = new Tuple<BitmapContainer, OnClick>(new BitmapContainer("http://www.psdgraphics.com/wp-content/uploads/2015/06/mosaic-background.png", null), click1);
            _imageList.Add(c1);
            OnClick click2 = new OnClick(() => { Toast.MakeText(AnatoliApp.GetInstance().Activity, "Item 2 selected", ToastLength.Short).Show(); });
            var c2 = new Tuple<BitmapContainer, OnClick>(new BitmapContainer("http://www.ayuz.ir/files/photos/s1.png", null), click2);
            _imageList.Add(c2);
            OnClick click3 = new OnClick(() => { Toast.MakeText(AnatoliApp.GetInstance().Activity, "Item 3 selected", ToastLength.Short).Show(); });
            var c3 = new Tuple<BitmapContainer, OnClick>(new BitmapContainer("https://pixabay.com/static/uploads/photo/2012/11/06/03/47/background-64259_960_720.jpg", null), click3);
            _imageList.Add(c3);

            mHandler = new Handler(async (msg) =>
            {
                if (msg.What == UPDATE_IMAGE)
                {
                    _imageIndex++;
                    if (_imageIndex >= _imageList.Count)
                    {
                        _imageIndex = 0;
                    }
                    Bitmap bitmap;
                    if (_imageList[_imageIndex].Item1.bitmap != null)
                    {
                        bitmap = _imageList[_imageIndex].Item1.bitmap;
                    }
                    else
                    {
                        bitmap = await Task.Run(() =>
                        {
                            return AnatoliAndroid.Extentions.ImageBitmapFromUrl(_imageList[_imageIndex].Item1.Path);
                        });
                    }
                    if (bitmap != null)
                    {
                        _imageList[_imageIndex].Item1.bitmap = bitmap;
                        _slideShow.SetImageBitmap(bitmap);
                    }
                    else
                        _slideShow.SetImageResource(Android.Resource.Drawable.ProgressIndeterminateHorizontal);
                    _slideShow.Click -= _slideShow_Click;
                    _slideShow.Click += _slideShow_Click;
                    ReloadImage();
                }
            });


            return view;
        }
        public override async void OnStart()
        {
            base.OnStart();
            AnatoliApp.GetInstance().HideSearchIcon();
            AnatoliApp.GetInstance().HideMenuIcon();
            await System.Threading.Tasks.Task.Run(() => { ReloadImage(); });
        }

        void _slideShow_Click(object sender, EventArgs e)
        {
            _imageList[_imageIndex].Item2();
        }
        public static int UPDATE_IMAGE = 1000;
        private Handler mHandler;
        void ReloadImage()
        {
            Message msg = new Message();
            msg.What = UPDATE_IMAGE;
            mHandler.SendMessageDelayed(msg, 6000);
        }

        delegate void OnClick();
    }
    class BitmapContainer
    {
        public String Path { get; set; }
        public Bitmap bitmap { get; set; }
        public BitmapContainer(string path, Bitmap bitmap)
        {
            Path = path;
            bitmap = bitmap;
        }
    }
}