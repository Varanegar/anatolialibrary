using Anatoli.Cloud.Gateway.Business.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/app/basket")]
    public class BasketInfoController : BaseApiController
    {
        [Authorize(Roles = "Admin")]
        [Route("baskets")]
        public IHttpActionResult GetBaskets()
        {
            return Ok(BasketInfoCloudHandler.GetInstance().GetSampleData());
        }
    }
}
