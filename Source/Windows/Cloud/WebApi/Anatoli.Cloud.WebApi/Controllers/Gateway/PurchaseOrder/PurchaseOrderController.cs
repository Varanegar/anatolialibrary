using Anatoli.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/purchaseorder")]
    public class PurchaseOrderController : ApiController
    {
        
        [Authorize(Roles = "AuthorizedApp")]
        [Route("history")]
        public IHttpActionResult GetPurchaseStatus()
        {
            return Ok();
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateOrder(PurchaseOrderViewModel orderEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return null;
        }
    }
}
