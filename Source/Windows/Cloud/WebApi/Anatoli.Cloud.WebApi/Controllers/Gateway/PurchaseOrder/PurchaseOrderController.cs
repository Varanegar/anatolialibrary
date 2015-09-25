using Aantoli.Common.Entity.Gateway.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/purchaseorder")]
    public class PurchaseOrderController : ApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateOrder(PurchaseOrderEntity orderEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        }
    }
}
