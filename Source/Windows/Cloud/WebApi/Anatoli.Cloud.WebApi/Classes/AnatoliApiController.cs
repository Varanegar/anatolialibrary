using System;
using System.Web;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.Cloud.WebApi.Controllers;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Cloud.WebApi.Classes
{
    public abstract class AnatoliApiController : BaseApiController
    {
        public class RequestModel
        {
            public string privateOwnerId { get; set; }
            public string stockId { get; set; }
            public string userId { get; set; }
            public string dateAfter { get; set; }
            public List<string> stockIds { get; set; }

            public string data { get; set; }
            public string ruleDate { get; set; }

            public string searchTerm { get; set; }
            public string stockRequestProduct { get; set; }
            public Guid ruleId { get; set; }

            public Guid stockProductRequestId { get; set; }
            public Guid stockProductRequestProductId { get; set; }

            public List<StockProductRequestProductViewModel> stockProductRequestProductList { get; set; }
            public List<ProductViewModel> Products  { get; set; }

    }

        public Guid OwnerKey
        {
            get
            {
                return Guid.Parse(HttpContext.Current.Request.Headers["OwnerKey"]);
            }
        }
    }
}