using Anatoli.Framework.AnatoliBase;
using Anatoli.App.Model.Store;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class StoreManager : BaseManager<BaseDataAdapter<StoreDataModel>, StoreDataModel>
    {
        //public async Task<List<StoreDataModel>> GetNearStores()
        //{
        //    var q = new Query(Configuration.WebService.Stores.StoresView, new Tuple<string, string>("count", count.ToString()), new Tuple<string, string>("q", "near"));
        //    return await GetNextAsync("SELECT * FROM stores", q);
        //}
        //public async Task<List<StoreDataModel>> GetAllAsync(int index, int offset)
        //{
        //    return await GetNextAsync(string.Format("SELECT * FROM stores LIMIT {0},{1}", index.ToString(), (index + offset).ToString()), null);
        //}
        //public async Task<List<StoreDataModel>> GetAllAsync(int index, int offset, int productId)
        //{
        //    return await GetNextAsync(string.Format("SELECT * FROM stores_products INNER JOIN stores WHERE stores.store_id == stores_products.store_id AND product_id = {0} LIMIT {1},{2}", productId.ToString(), index.ToString(), (index + offset).ToString()), null);
        //}
        protected override string GetDataTable()
        {
            return "stores";
        }

        protected override string GetWebServiceUri()
        {
            return Configuration.WebService.Stores.StoresView;
        }
    }
}
