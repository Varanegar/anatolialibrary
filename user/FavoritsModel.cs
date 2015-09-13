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
    class FavoritsModel : SyncDataModel
    {
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
                _products[product] -= count;
            }
            catch (Exception)
            {

            }
        }
        public override void LocalSaveAsync()
        {
            throw new NotImplementedException();
        }

        public override void CloudSaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
