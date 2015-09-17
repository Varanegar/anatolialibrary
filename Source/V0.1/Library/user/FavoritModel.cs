using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    [Table("FavoritModel")]
    public class FavoritModel
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Count { get; set; }
        public FavoritModel() { }
        public FavoritModel(string userId, string productId, int count)
        {
            UserId = userId;
            ProductId = productId;
            Count = count;
        }
        public static string TableName = "FavoritModel";
    }
}
