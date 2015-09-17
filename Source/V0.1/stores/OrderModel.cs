using AnatoliaLibrary.products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.stores
{
    public class OrderModel
    {
        CheckOutModel _checkout;
        DeliveryModel _delivary;
        StoreModel _store;
        Dictionary<ProductModel, int> _products;
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
        public void RemoveItem(ProductModel product, int count)
        {
            if (_products.ContainsKey(product))
            {
                int cnt = _products[product] - count;
                if (cnt > 0)
                    _products[product] = cnt;
                else
                    _products.Remove(product);
            }
        }
        public void RemoveItem(ProductModel product)
        {
            if (_products.ContainsKey(product))
            {
                _products.Remove(product);
            }
        }

    }
}
