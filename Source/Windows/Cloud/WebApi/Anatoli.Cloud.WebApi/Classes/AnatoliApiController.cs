using System;
using System.Web;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Classes
{
    public class AnatoliApiController: ApiController
    {
        public class RequestModel
        {
            public string privateOwnerId { get; set; }
            public string stockId { get; set; }
            public string userId { get; set; }
            public string dateAfter { get; set; }
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