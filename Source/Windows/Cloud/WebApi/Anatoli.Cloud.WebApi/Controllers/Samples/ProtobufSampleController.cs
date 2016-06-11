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
using System.Text;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;

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
        public async Task<List<ProductViewModel>> GetProductsTest()
        {
            try
            {
                var productDomain = new Business.Domain.ProductDomain(Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
                                        Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"),
                                        Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"));
                var result = await productDomain.GetAllAsync();

                return result;

                //var id = Guid.NewGuid();

                //var model = new ProductViewModel
                //{
                //    UniqueId = id,
                //    ProductName = "test Mrg",
                //    Barcode = "ba",
                //    Desctription = "dsf",
                //    IsActiveInOrder = true,
                //    MainSupplierName = "sf",
                //    PackWeight = 125,
                //    ProductCode = "sf234234swa",
                //    ProductRate = 5,
                //    QtyPerPack = 6,
                //    StoreProductName = "test Mrg1234",
                //    ProductTypeInfo = new ProductTypeViewModel
                //    {
                //        UniqueId = id,
                //        ProductTypeName = "type1"
                //    },
                //    ProductTypeId = id
                //};

                //return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("sendingGzipSample")]
        public async Task<IHttpActionResult> SendingGzipSample()
        {
            try
            {
                var id = Guid.NewGuid();

                var model = new List<ProductViewModel>
                {
                    new ProductViewModel
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
                    }
                };

                var results = new List<ProductViewModel>();

                using (HttpClientHandler handler = new HttpClientHandler())
                {
                    handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;

                    using (HttpClient client = new HttpClient(handler, false))
                    {
                        var json = JsonConvert.SerializeObject(model);
                        var jsonBytes = Encoding.UTF8.GetBytes(json);
                        var ms = new MemoryStream();

                        using (var gzip = new GZipStream(ms, CompressionMode.Compress, true))
                        {
                            gzip.Write(jsonBytes, 0, jsonBytes.Length);
                        }

                        ms.Position = 0;

                        var content = new StreamContent(ms);

                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        content.Headers.ContentEncoding.Add("gzip");

                        var response = await client.PostAsync("http://localhost:59822/api/ProtobufSample/recievingGzipModel", content);

                        results = await response.Content.ReadAsAsync<List<ProductViewModel>>();
                    }
                }
                return Ok(results);

            }
            catch (Exception ex)
            {
                throw ex;
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
        [Route("recievingGzipModel"), HttpPost]
        public async Task<IHttpActionResult> RecievingGzipModel([GzipBody] List<ProductViewModel> model)
        {
            return Ok(model);
        }
    }
}
