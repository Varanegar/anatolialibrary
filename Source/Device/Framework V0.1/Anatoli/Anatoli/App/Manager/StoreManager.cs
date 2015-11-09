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
        public static async Task<bool> SelectAsync(StoreDataModel store)
        {
            UpdateCommand command1 = new UpdateCommand("stores", new BasicParam("selected", "0"));
            UpdateCommand command2 = new UpdateCommand("stores", new BasicParam("selected", "1"), new EqFilterParam("store_id", store.store_id.ToString()));
            try
            {
                int clear = await LocalUpdateAsync(command1);
                int result = await LocalUpdateAsync(command2);
                return (result > 0) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<StoreDataModel> GetDefault()
        {
            SelectQuery query = new SelectQuery("stores", new EqFilterParam("selected", "1"));
            try
            {
                var store = await GetItemAsync(query);
                if (store == null)
                    throw new NullStoreException();
                return store;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class NullStoreException : Exception
        {
        }
    }
}
