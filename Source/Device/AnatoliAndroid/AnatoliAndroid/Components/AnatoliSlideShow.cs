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

namespace AnatoliAndroid.Components
{
    class AnatoliSlideShow
    {
        int _imageIndex = 0;
        bool _continue = true;
        public List<Tuple<BitmapContainer, OnClick>> Source = new List<Tuple<BitmapContainer, OnClick>>();
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
            mHandler = new Handler(async (msg) =>
            {
                if (msg.What == UPDATE_IMAGE)
                {
                    _imageIndex++;
                    if (_imageIndex >= Source.Count)
                    {
                        _imageIndex = 0;
                    }
                    Bitmap bitmap;
                    if (Source[_imageIndex].Item1.bitmap != null)
                    {
                        bitmap = Source[_imageIndex].Item1.bitmap;
                    }
                    else
                    {
                        _imageView.SetImageResource(Android.Resource.Drawable.ProgressIndeterminateHorizontal);
                        progress.Visibility = ViewStates.Visible;
                        bitmap = await Task.Run(() =>
                        {
                            return AnatoliAndroid.Extentions.ImageBitmapFromUrl(Source[_imageIndex].Item1.Path, Delay);
                        });
                        progress.Visibility = ViewStates.Invisible;
                    }
                    if (bitmap != null)
                    {
                        Source[_imageIndex].Item1.bitmap = bitmap;
                        _imageView.SetImageBitmap(bitmap);
                    }
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
    class BitmapContainer
    {
        public String Path { get; set; }
        public Bitmap bitmap { get; set; }
        public BitmapContainer(string path, Bitmap bitmapImage)
        {
            Path = path;
            bitmap = bitmapImage;
        }
    }
}