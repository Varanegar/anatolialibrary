using System;
using System.Net;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;

namespace Mvc_Web.Controllers
{
    [RoutePrefix("api/ProductManager")]
    public class ProductManagerController : ApiController
    {
        #region Inner Classes
        public class Store
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        public class Product
        {
            public Guid Id { get; set; }
            public string ProductName { get; set; }
            public int LargeUnit { get; set; }
            public int SmallUnit { get; set; }

            public Store Store { get; set; }
        }
        public class ProductRequest
        {
            public Guid Id { get; set; }
            public string ModifiedBy { get; set; }
            public string ModifiedDate { get; set; }
            public int LargeUnit { get; set; }
            public int SmallUnit { get; set; }

            public Product Product { get; set; }
        }
        public class StoreRequest
        {
            public Guid Id { get; set; }
            public string RequestDate { get; set; }
            public string RequestNumber { get; set; }
            public string State { get; set; }
            public Store Store { get; set; }

        }
        public class RequestModel
        {
            public string StoreId { get; set; }
            public string ProductId { get; set; }
            public string StoreRequestId { get; set; }
        }
        #endregion

        #region Properties
        public static List<Store> Stores { get; set; }
        public static List<Product> Products { get; set; }
        public static List<ProductRequest> ProductRequestsHistory { get; set; }
        public static List<StoreRequest> StoreRequests { get; set; }
        #endregion

        #region Ctors
        public ProductManagerController()
        {
            if (Stores == null)
            {
                Stores = new List<Store>();
                for (int i = 0; i < 5; i++)
                    Stores.Add(new Store { Id = Guid.NewGuid(), Name = "فروشگاه " + (i + 1) });

                Products = new List<Product>();
                for (int i = 0; i < 50; i++)
                    Products.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        ProductName = "محصول " + (i + 1),
                        LargeUnit = new Random((int)DateTime.Now.Ticks).Next(1, 5),
                        SmallUnit = new Random((int)DateTime.Now.Ticks).Next(1, 50),
                        Store = Stores[new Random((int)DateTime.Now.Ticks).Next(0, 4)]
                    });

                ProductRequestsHistory = new List<ProductRequest>();
                for (int i = 0; i < 50; i++)
                    ProductRequestsHistory.Add(new ProductRequest
                    {
                        Id = Guid.NewGuid(),
                        ModifiedBy = "درخواست دهنده " + (new Random((int)DateTime.Now.Ticks).Next(1, 5)),
                        LargeUnit = new Random((int)DateTime.Now.Ticks).Next(1, 50),
                        SmallUnit = new Random((int)DateTime.Now.Ticks).Next(1, 50),
                        ModifiedDate = DateTime.Now.ToString(),
                        Product = Products[new Random((int)DateTime.Now.Ticks).Next(0, 49)]
                    });

                StoreRequests = new List<StoreRequest>();
                for (int i = 0; i < 100; i++)
                    StoreRequests.Add(new StoreRequest
                    {
                        Id = Guid.NewGuid(),
                        RequestDate = DateTime.Now.ToShortDateString(),
                        RequestNumber = new Random((int)DateTime.Now.Ticks).Next(1, 10203040).ToString(),
                        State = "فوری",

                        Store = Stores[new Random((int)DateTime.Now.Ticks).Next(0, 4)]
                    });
            }
        }
        #endregion

        #region Methods
        [HttpGet]
        public HttpResponseMessage GetStores()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Stores);

            //response.Headers.CacheControl = new CacheControlHeaderValue()
            //{
            //    MaxAge = TimeSpan.FromMinutes(20)
            //};
        }

        [HttpPost, Route("GetProducts")]
        public HttpResponseMessage GetProducts([FromBody] RequestModel data)
        {
            var model = new List<Product>();

            if (!string.IsNullOrEmpty(data.StoreId))
                model = Products.Where(p => p.Store.Id == Guid.Parse(data.StoreId)).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPost, Route("UpdateProducts")]
        public HttpResponseMessage UpdateProducts([FromBody] List<Product> models)
        {
            models.ForEach(itm =>
            {
                var indx = Products.FindIndex(p => p.Id == itm.Id);

                Products[indx] = itm;
            });

            return Request.CreateResponse(HttpStatusCode.OK, Products);
        }

        [HttpPost, Route("DestoryProducts")]
        public HttpResponseMessage DestoryProducts([FromBody] List<Product> models)
        {
            models.ForEach(itm => Products.Remove(itm));

            return Request.CreateResponse(HttpStatusCode.OK, Products);
        }

        [HttpPost, Route("GetProductRequestsHistory")]
        public HttpResponseMessage GetProductRequestsHistory([FromBody] RequestModel data)
        {
            var model = new List<ProductRequest>();

            if (!string.IsNullOrEmpty(data.ProductId))
                model = ProductRequestsHistory.Where(p => p.Product.Id == Guid.Parse(data.ProductId)).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPost, Route("GetStoreRequests")]
        public HttpResponseMessage GetStoreRequests()
        {
            return Request.CreateResponse(HttpStatusCode.OK, StoreRequests.Select(s => new
            {
                s.Id,
                s.RequestDate,
                s.RequestNumber,
                StoreName = s.Store.Name
            }).ToList());
        }

        [HttpPost, Route("GetStoreRequestInfo")]
        public HttpResponseMessage GetStoreRequestInfo([FromBody] RequestModel data)
        {
            var model = StoreRequests.Where(p => p.Id == Guid.Parse(data.StoreRequestId))
                                     .Select(s => new { StoreName = s.Store.Name, s.RequestNumber, s.RequestDate, s.State })
                                     .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
        #endregion
    }
}
