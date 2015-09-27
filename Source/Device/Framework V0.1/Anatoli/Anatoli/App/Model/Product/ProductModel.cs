using Anatoli.Framework.Model;
using Anatoli.Anatoliclient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Model.Product
{
    public class ProductModel : UpdateOnlyDataModel
    {
        protected int _rateCount;
        public int RateCount
        {
            get { return _rateCount; }
        }
        public string Name { get; set; }
        public class ProductName
        {
            public string Name { get; set; }
            public static implicit operator ProductName(string name)
            {
                var p = new ProductName();
                p.Name = name;
                return p;
            }
            public static implicit operator string(ProductName pname)
            {
                return pname.Name;
            }
        }

        public override void CloudUpdateAsync()
        {
            // update from cloud
            Name = "Updated from cloud";
        }

        public override void LocalUpdateAsync()
        {
            // update from sqlite database
            Name += " Updated from local database";
            var connection = AnatoliClient.GetInstance().DbClient.Connection;
            var query = connection.Table<ProductModel>().Where(d => ID == d.ID);
            try
            {
                if (query.Count() == 1)
                {
                    var p = query.First();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
