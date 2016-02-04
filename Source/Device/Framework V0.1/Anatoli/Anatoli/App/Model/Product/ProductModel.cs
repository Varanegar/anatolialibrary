using Anatoli.Framework.Model;
using Anatoli.Framework.AnatoliBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Anatoli.App.Model.Product
{
    [Table("products")]
    public class ProductModel : BaseDataModel
    {
        public int order_count { get; set; }
        public string cat_id { get; set; }
        public string cat_name { get; set; }
        public int brand_id { get; set; }
        public string product_name { get; set; }
        public string product_id { get; set; }
        public double price { get; set; }
        public int favorit { get; set; }
        public int count { get; set; }
        public string image { get; set; }
        public int is_group { get; set; }
        public bool IsGroup
        {
            get { return is_group == 1 ? true : false; }
        }
        public bool IsFavorit
        {
            get { return favorit == 1 ? true : false; }
        }
    }
    public static class DoubleExtension
    {
        public static string ToCurrency(this double value)
        {
            return Math.Round(value / 10, 0).ToString("N0");
        }
    }
}
