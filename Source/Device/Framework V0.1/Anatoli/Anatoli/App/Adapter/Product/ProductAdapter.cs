using Anatoli.Anatoliclient;
using Anatoli.App.Model.Product;
using Anatoli.Framework.DataAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Adapter.Product
{
    public class ProductAdapter : BaseDataAdapter<ProductListModel, ProductModel>
    {
        public override void CloudUpdate()
        {
            // update from cloud
            try
            {
                var response = AnatoliClient.GetInstance().WebClient.SendGetRequest<ProductModelUpdateResult>(
                Configuration.WebService.Products.ProductView,
                new Tuple<string, string>("ID", DataModel.UniqueId.ToString())
                );
                if (response.metaInfo.Result)
                {
                    DataModel = response.productModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void LocalUpdate()
        {
            // update from sqlite database
            DataModel.Name += " Updated from local database";
            var connection = AnatoliClient.GetInstance().DbClient.Connection;
            var query = connection.Table<ProductModel>().Where(d => DataModel.ID == d.ID);
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


        public override bool IsDataIDValid(string ID)
        {
            return true;
        }
    }
}
