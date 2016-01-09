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
using AnatoliAndroid.Fragments;
using Android.Graphics;
using System.Net;

namespace AnatoliAndroid
{
    public static class Extentions
    {
        public static string GetTitle(this Fragment fragment)
        {
            var title = Attribute.GetCustomAttribute(fragment.GetType(), typeof(FragmentTitle));
            if (title != null)
                return (title as FragmentTitle).Title;
            else
                return "";
        }

        public static void UpdateWidth(this Button button)
        {
            Android.Util.DisplayMetrics metrics = new Android.Util.DisplayMetrics();
            AnatoliAndroid.Activities.AnatoliApp.GetInstance().Activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            var width = 2 * (metrics.WidthPixels / 4);
            var pixels = metrics.Xdpi * 2;
            width = ((float)width > pixels) ? (int)Math.Round(pixels) : width;
            button.SetWidth(width);
        }
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }

        public static Bitmap ImageBitmapFromUrl(string url)
        {
            try
            {
                var request = HttpWebRequest.Create(url);
                request.Timeout = 10000;
                Bitmap imageBitmap = null;
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        byte[] imageBytes = new byte[2097152];
                        int totalBytes = 0;
                        int bytesRead;
                        do
                        {
                            bytesRead = stream.Read(imageBytes, 0, imageBytes.Length);
                            totalBytes += bytesRead;
                        } while (bytesRead != 0);
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, totalBytes);
                    }
                }
                return imageBitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}