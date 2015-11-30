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
                    if ((_upTime - _downTime) > 1000 && d < 30)
                        OnLongClick();
                    else if ((_upTime - _downTime) > 100 && d > 100)
                    {
                        if ((e.RawX - _downX) < -100)
                            OnSwipeLeft();
                        else if ((e.RawX - _downX) > 100)
                            OnSwipeRight();
                    }
                    else
                        OnClick();
                    break;
                case MotionEventActions.Down:
                    _downTime = Now();
                    _downX = e.RawX;
                    _downY = e.RawY;
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

        void OnSwipeLeft()
        {
            if (SwipeLeft != null)
            {
                SwipeLeft.Invoke(this, new EventArgs());
            }
        }
        public event EventHandler SwipeLeft;

        void OnSwipeRight()
        {
            if (SwipeRight != null)
            {
                SwipeRight.Invoke(this, new EventArgs());
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