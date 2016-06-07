using System;
using ProtoBuf;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using WebApiContrib.Formatting;
using System.Collections.Generic;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Cloud.WebApi.Controllers.Samples
{
    /// <summary>
    /// To test in fiddler Composer: json Content-Length: 1275, protobuf Content-Length: 202
    /// http://localhost:59822/api/ProtobufSample/productsTest
    /// User-Agent: Fiddler
    /// Host: localhost:59822
    /// Accept: application/x-protobuf
    /// </summary>
    [RoutePrefix("api/ProtobufSample")]
    public class ProtobufSampleController : ApiController
    {
        [ProtoContract]
        public class DataRequest
        {
            [ProtoMember(1)]
            public ProductViewModel p { get; set; }
            [ProtoMember(2)]
            public List<ProductViewModel> ps { get; set; }
        }

        /// <summary>
        /// Data could send in protobuf binary format, and custome media formatter will deserilized it into model
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("saveProducts")]
        [HttpPost]
        public IHttpActionResult SaveProducts([FromBody] DataRequest data)
        {
            return Ok();
        }

        /// <summary>
        /// or you could deserialize data from request content
        /// </summary>
        /// <returns></returns>
        [Route("saveProducts1")]
        [HttpPost]
        public IHttpActionResult SaveProducts1()
        {
            var data = Request.Content.ReadAsStreamAsync().Result;

            var model = Serializer.Deserialize<DataRequest>(data);

            return Ok(model);
        }

        /// <summary>
        /// sample to get products model with HttpClient in backend, and call the save method
        /// </summary>
        /// <returns></returns>
        [Route("getProducts")]
        public IHttpActionResult GetProducts()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:59822/") };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

            HttpResponseMessage response = client.GetAsync("api/ProtobufSample/productsTest").Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var p = response.Content.ReadAsAsync<ProductViewModel>(new[] { new ProtoBufFormatter() }).Result;

                testSavingProducts(p);

                return Ok(p);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return Ok();
            }
        }

        /// <summary>
        /// sample to serialize and send data in protobuf format to backend.
        /// </summary>
        /// <param name="p"></param>
        private void testSavingProducts(ProductViewModel p)
        {
            try
            {
                var client = new HttpClient { BaseAddress = new Uri("http://localhost:59822/") };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

                Task.Factory.StartNew(() =>
                {
                    var resp = client.PostAsync("api/ProtobufSample/saveProducts", new { p, ps = new List<ProductViewModel> { p } }, new ProtoBufFormatter());
                }).Wait();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// sample to get model in any formats like: headers Accept: 'application/x-protobuf'
        /// </summary>
        /// <returns></returns>
        [Route("productsTest")]
        public ProductViewModel GetProductsTest()
        {
            try
            {
                var id = Guid.NewGuid();

                var model = new ProductViewModel
                {
                    UniqueId = id,
                    ProductName = "test Mrg",
                    Barcode = "ba",
                    Desctription = "dsf",
                    IsActiveInOrder = true,
                    MainSupplierName = "sf",
                    PackWeight = 125,
                    ProductCode = "sf234234swa",
                    ProductRate = 5,
                    QtyPerPack = 6,
                    StoreProductName = "test Mrg1234",
                    ProductTypeInfo = new ProductTypeViewModel
                    {
                        UniqueId = id,
                        ProductTypeName = "type1"
                    },
                    ProductTypeId = id
                };

                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AnatoliAuthorize,Route("productsTest2")]
        public ProductViewModel GetProductsTest2()
        {
            try
            {
                var id = Guid.NewGuid();

                var model = new ProductViewModel
                {
                    UniqueId = id,
                    ProductName = "test Mrg",
                    Barcode = "ba",
                    Desctription = "dsf",
                    IsActiveInOrder = true,
                    MainSupplierName = "sf",
                    PackWeight = 125,
                    ProductCode = "sf234234swa",
                    ProductRate = 5,
                    QtyPerPack = 6,
                    StoreProductName = "test Mrg1234",
                    ProductTypeInfo = new ProductTypeViewModel
                    {
                        UniqueId = id,
                        ProductTypeName = "type1"
                    },
                    ProductTypeId = id
                };

                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
