using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anatoli.App.Model.Product;

namespace Anatoli.App.Model.AnatoliUser
{
    public class ShoppingCard
    {
        public Dictionary<int, ShoppingCardItem> Items;
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
            Items = new Dictionary<int, ShoppingCardItem>();
        }
    }
    public class ShoppingCardItem
    {
        public ShoppingCardItem(int count, ProductModel productModel)
        {
            this.productModel = productModel;
            this.Count = count;
        }
        public ProductModel productModel { get; set; }
        public int Count { get; set; }
    }
}