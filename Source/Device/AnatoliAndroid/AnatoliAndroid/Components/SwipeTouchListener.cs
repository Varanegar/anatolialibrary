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

namespace AnatoliAndroid.Components
{
    public class SwipeTouchListener : Java.Lang.Object, View.IOnTouchListener
    {

        public GestureDetector gestureDetector;
        private Context context;

        public SwipeTouchListener(Context ctx)
        {
            gestureDetector = new GestureDetector(ctx, new GestureListener(this));
            context = ctx;
        }
        public bool OnTouch(View v, MotionEvent e)
        {
            return gestureDetector.OnTouchEvent(e);
        }

        void OnSwipeLeft()
        {
            if (SwipeLeft != null)
            {
                SwipeLeft.Invoke(this);
            }
        }
        public event SwipeLeftEventHandler SwipeLeft;
        public delegate void SwipeLeftEventHandler(object sender);
        void OnSwipeRight()
        {
            if (SwipeRight != null)
            {
                SwipeRight.Invoke(this);
            }
        }
        public event SwipeRightEventHandler SwipeRight;
        public delegate void SwipeRightEventHandler(object sender);

        public class GestureListener : Java.Lang.Object, GestureDetector.IOnGestureListener
        {

            private static int SWIPE_THRESHOLD = 100;
            private static int SWIPE_VELOCITY_THRESHOLD = 100;

            SwipeTouchListener _listener;
            public GestureListener(SwipeTouchListener listener)
            {
                _listener = listener;
            }

            public bool OnDown(MotionEvent e)
            {
                return true;
            }

            public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
            {
                float distanceX = e2.GetX() - e1.GetX();
                float distanceY = e2.GetY() - e1.GetY();
                if (System.Math.Abs(distanceX) > System.Math.Abs(distanceY) && System.Math.Abs(distanceX) > SWIPE_THRESHOLD && System.Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
                {
                    if (distanceX > 0)
                        _listener.OnSwipeRight();
                    else
                        _listener.OnSwipeLeft();
                    return true;
                }
                return false;
            }

            public void OnLongPress(MotionEvent e)
            {

            }

            public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
            {
                return true;
            }

            public void OnShowPress(MotionEvent e)
            {

            }

            public bool OnSingleTapUp(MotionEvent e)
            {
                return true;
            }

        }
    }
}