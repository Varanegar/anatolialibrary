using AnatoliaLibrary.anatoliaclient;
using AnatoliaLibrary.user;
using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnatoliaLibrary.products
{
    public class ProductManager
    {
        AnatoliaClient _client;
        public ProductManager(AnatoliaClient client)
        {
            _client = client;
        }
        public static async Task<ProductModel> GetProductAsync(string productId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets Frequently Ordered Goods
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductModel>> GetFOGAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets Frequently Ordered Goods of type pType
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public async Task<List<ProductModel>> GetFOGAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ProductModel>> ListProductsAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ProductType>> ListProductTypesAsync(ProductType pType)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ProductModel>> SearchAsync(AnatoliaLibrary.products.ProductModel.ProductName name)
        {
            throw new NotImplementedException();
        }
        public async Task<RateResponse> SaveRateAsync(AnatoliaUserModel user, ProductModel product, int rate)
        {
            var client = new RestClient(Configuration.PortalUri);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest(String.Format("resource/{1}", Configuration.RateProductUri), HttpMethod.Post);
            request.AddParameter("product_id", product.Id);
            request.AddParameter("rate", rate);
            request.AddParameter("user_id", user.UserId);
            var response = await client.Execute<RateResponse>(request);
            return response.Data;
        }

        public class RateResponse
        {
            public int RateCount { get; set; }
            public bool Result { get; set; }
        }
    }
}
