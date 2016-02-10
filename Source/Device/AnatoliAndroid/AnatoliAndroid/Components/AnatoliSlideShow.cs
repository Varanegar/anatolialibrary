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
using System.Threading.Tasks;
using Anatoli.Framework.AnatoliBase;
using System.IO;
using Square.Picasso;
using AnatoliAndroid.Activities;

namespace AnatoliAndroid.Components
{
    class AnatoliSlideShow
    {
        int _imageIndex = 0;
        bool _continue = true;
        public List<Tuple<string, OnClick>> Source = new List<Tuple<string, OnClick>>();
        public delegate void OnClick();
        public static int UPDATE_IMAGE = 1000;
        bool _first = true;
        public int Delay = 10000;
        private Handler mHandler;
        ImageView _imageView;
        public AnatoliSlideShow(ImageView imageView, ProgressBar progress)
        {
            _imageView = imageView;
            _imageView.Click += _imageView_Click;
            mHandler = new Handler((msg) =>
            {
                if (msg.What == UPDATE_IMAGE)
                {
                    _imageIndex++;
                    if (_imageIndex >= Source.Count)
                    {
                        _imageIndex = 0;
                    }
                    progress.Visibility = ViewStates.Visible;
                    
                    Picasso.With(AnatoliApp.GetInstance().Activity).Load( Source[_imageIndex].Item1).Into(_imageView);
                    progress.Visibility = ViewStates.Invisible;
                    if (_continue)
                    {
                        Start();
                    }
                }
            });
        }
        public void Start()
        {
            Message msg = new Message();
            msg.What = UPDATE_IMAGE;
            if (_first)
            {
                mHandler.SendMessageDelayed(msg, 1);
                _first = false;
            }
            else
                mHandler.SendMessageDelayed(msg, Delay);
        }

        public void Stop()
        {
            _continue = false;
        }
        void _imageView_Click(object sender, EventArgs e)
        {
            if (Source[_imageIndex].Item2 != null)
                Source[_imageIndex].Item2();
        }

    }
}