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
    class OnTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        long _downTime;
        long _upTime;
        float _downX;
        float _downY;
        public bool OnTouch(View v, MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Up:
                    _upTime = Now();
                    var d = Math.Abs(e.RawX - _downX);
                    var dd = Android.Content.Res.Resources.System.DisplayMetrics.WidthPixels / d;
                    if ((_upTime - _downTime) > 2000 && d < 10)
                        OnLongClick();
                    else if ((_upTime - _downTime) > 100 && dd < 10)
                    {
                        if ((e.RawX - _downX) < -20)
                            OnSwipeLeft(v);
                        else if ((e.RawX - _downX) > 20)
                            OnSwipeRight(v);
                    }
                    else
                        OnClick();
                    break;
                case MotionEventActions.Down:
                    _downTime = Now();
                    _downX = e.RawX;
                    _downY = e.RawY;
                    break;
                case MotionEventActions.Cancel:
                    _upTime = Now();
                    d = Math.Abs(e.RawX - _downX);
                    dd = Android.Content.Res.Resources.System.DisplayMetrics.WidthPixels / d;
                    if ((_upTime - _downTime) > 2000 && d < 10)
                        OnLongClick();
                    else if ((_upTime - _downTime) > 100 && dd < 10)
                    {
                        if ((e.RawX - _downX) < -20)
                            OnSwipeLeft(v);
                        else if ((e.RawX - _downX) > 20)
                            OnSwipeRight(v);
                    }
                    else
                        OnClick();
                    break;
                default:
                    break;
            }
            return true;
        }

        void OnClick()
        {
            if (Click != null)
            {
                Click.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler Click;

        void OnLongClick()
        {
            if (LongClick != null)
            {
                LongClick.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler LongClick;

        void OnSwipeLeft(object sender)
        {
            Console.WriteLine("Swipe left");
            if (SwipeLeft != null)
            {
                SwipeLeft.Invoke(sender, new EventArgs());
            }
        }
        public event EventHandler SwipeLeft;

        void OnSwipeRight(object sender)
        {
            Console.WriteLine("Swipe right");
            if (SwipeRight != null)
            {
                SwipeRight.Invoke(sender, new EventArgs());
            }
        }
        public event EventHandler SwipeRight;
        long Now()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return milliseconds;
        }
    }
}