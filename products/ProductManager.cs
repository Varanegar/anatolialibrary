using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.products
{
    public class ProductManager
    {
        public static async Task<ProductModel> GetProductAsync(string productId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets Frequently Ordered Goods
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ProductModel>> GetFOGAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets Frequently Ordered Goods of type pType
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public static async Task<List<ProductModel>> GetFOGAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public static async Task<List<ProductModel>> ListProductsAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public static async Task<List<ProductType>> ListProductTypesAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public static async Task<List<ProductModel>> SearchAsync(AnatoliaLibrary.products.ProductModel.ProductName name)
        {
            throw new NotImplementedException();
        }
    }
}
