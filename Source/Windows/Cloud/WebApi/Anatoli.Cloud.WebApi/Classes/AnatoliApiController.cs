using System;
using System.Web;
using System.Collections.Generic;
using Anatoli.Cloud.WebApi.Controllers;

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