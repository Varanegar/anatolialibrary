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

namespace AnatoliAndroid
{
    class ShoppingCard
    {
        public Dictionary<int, int> Items;
        private static ShoppingCard instance;
        public static ShoppingCard GetInstance()
        {
            if (instance == null)
            {
                instance = new ShoppingCard();
                return instance;
            }
            else
                return instance;
        }
        private ShoppingCard()
        {
            Items = new Dictionary<int, int>();
        }
    }
}