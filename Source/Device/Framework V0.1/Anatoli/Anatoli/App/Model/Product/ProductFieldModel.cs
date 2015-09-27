using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Product
{
    [Table("ProductFieldModel")]
    public class ProductFieldModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Id { get; set; }
        public static string TableName = "ProductFieldModel";
        public ProductFieldModel() { }
        public ProductFieldModel(string name, string type, string value, string id)
        {
            Name = name;
            Type = type;
            Value = value;
            Id = id;
        }
    }
}
