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
using Anatoli.App.Model.Product;

namespace AnatoliAndroid.Fragments
{
    class JProductModel : Java.Lang.Object
    {
        public ProductModel model { get; set; }
        public JProductModel(ProductModel item)
        {
            model = item;
        }
    }
}