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
using Android.Graphics;
using Android.Graphics.Drawables;

namespace AnatoliAndroid.Components
{
    public class RoundedImageView : ImageView
    {

        public RoundedImageView(Context context)
            : base(context)
        {
            // TODO Auto-generated constructor stub
        }

        public RoundedImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public RoundedImageView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }
        public RoundedImageView(IntPtr a, JniHandleOwnership b)
            : base(a, b)
        {
         
        }
        override public void Draw(Canvas canvas)
        {

            Android.Graphics.Drawables.Drawable drawable = Drawable;

            if (drawable == null)
            {
                return;
            }

            if (Width == 0 || Height == 0)
            {
                return;
            }

            Bitmap b = null;
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop
                && drawable is VectorDrawable)
            {
                ((VectorDrawable)drawable).Draw(canvas);
                b = Bitmap.CreateBitmap(canvas.Width, canvas.Height, Bitmap.Config.Argb8888);
                Canvas c = new Canvas();
                c.SetBitmap(b);
                drawable.Draw(c);
            }
            else
            {
                b = ((BitmapDrawable)drawable).Bitmap;
            }

            Bitmap bitmap = b.Copy(Bitmap.Config.Argb8888, true);

            int w = Width, h = Height;

            Bitmap roundBitmap = getCroppedBitmap(bitmap, w);
            canvas.DrawBitmap(roundBitmap, 0, 0, null);
        }

        public static Bitmap getCroppedBitmap(Bitmap bmp, int radius)
        {
            Bitmap sbmp;
            if (bmp.Width != radius || bmp.Height != radius)
                sbmp = Bitmap.CreateScaledBitmap(bmp, radius, radius, false);
            else
                sbmp = bmp;
            Bitmap output = Bitmap.CreateBitmap(sbmp.Width,
                    sbmp.Height, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(output);

            uint color = 0xffa19774;
            Paint paint = new Paint();
            Rect rect = new Rect(0, 0, sbmp.Width, sbmp.Height);

            paint.AntiAlias = true;
            paint.FilterBitmap = true;
            paint.Dither = true;
            canvas.DrawARGB(0, 0, 0, 0);
            paint.Color = Color.ParseColor("#BAB399");
            canvas.DrawCircle(sbmp.Width / 2 + 0.7f, sbmp.Height / 2 + 0.7f,
                    sbmp.Width / 2 + 0.1f, paint);
            paint.SetXfermode(new PorterDuffXfermode(Android.Graphics.PorterDuff.Mode.SrcIn));
            canvas.DrawBitmap(sbmp, rect, rect, paint);

            return output;
        }
    }
}