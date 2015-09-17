using AnatoliaLibrary.anatoliaclient;
using AnatoliaLibrary.products;
using AnatoliaLibrary.stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.user
{
    public class FavoritsModel : SyncDataModel
    {
        AnatoliaUserModel _user;
        public FavoritsModel(AnatoliaClient client, AnatoliaUserModel user)
            : base(client)
        {
            _products = new Dictionary<ProductModel, int>();
            _user = user;
            if (_client.DbClient.TableExists(FavoritModel.TableName))
            {
                var table = _client.DbClient.Connection.Table<FavoritModel>();
                var query = table.Where(fm => true);
                foreach (var item in query)
                {
                    _products.Add(new ProductModel(item.ProductId), item.Count);
                }
            }
        }
        Dictionary<ProductModel, int> _products;
        public Dictionary<ProductModel, int> Products
        {
            get { return _products; }
        }
        public void AddItem(ProductModel product)
        {
            if (_products.ContainsKey(product))
                _products[product]++;
            else
                _products.Add(product, 1);
        }
        public void AddItem(ProductModel product, int count)
        {
            if (_products.ContainsKey(product))
                _products[product] += count;
            else
                _products.Add(product, count);
        }
        public void RemoveItem(ProductModel product)
        {
            try
            {
                _products.Remove(product);
            }
            catch (Exception)
            {

            }
        }
        public void RemoveItem(ProductModel product, int count)
        {
            try
            {
                int cnt = _products[product] - count;
                if (cnt > 0)
                    _products[product] = cnt;
                else
                    _products.Remove(product);
            }
            catch (Exception)
            {

            }
        }
        public override void LocalSaveAsync()
        {
            var connection = _client.DbClient.GetConnection();
            if (!_client.DbClient.TableExists(FavoritModel.TableName))
            {
                connection.CreateTable<FavoritModel>();
            }
            foreach (var item in _products)
            {
                var fm = new FavoritModel(_user.UserId, item.Key.Id, item.Value);
                connection.Insert(fm);
            }
        }

        public override void CloudSaveAsync()
        {
            return;
        }
    }
}
