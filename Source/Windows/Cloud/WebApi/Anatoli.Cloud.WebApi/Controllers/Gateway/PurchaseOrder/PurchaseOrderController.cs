using Anatoli.Business.Domain;
using Anatoli.PMC.Business.Domain.PurchaseOrder;
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

        [Authorize(Roles = "User")]
        [Route("history")]
        public IHttpActionResult GetPurchaseStatus()
        {
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateOrder(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            var owner = Guid.Parse(privateOwnerId);
            var orderDomain = new PurchaseOrderDomain(owner);
            await orderDomain.PublishOrderOnline(orderEntity);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("local/create")]
        public async Task<IHttpActionResult> CreateOrderLocal(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            PurchaseOrderViewModel result = new PurchaseOrderViewModel();
            await Task.Factory.StartNew(() =>
            {
                var orderDomain = new PMCPurchaseOrderDomain();
                result = orderDomain.Publish(orderEntity);
            });
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [Route("calcpromo")]
        public async Task<IHttpActionResult> ClacPromo(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            var owner = Guid.Parse(privateOwnerId);
            var orderDomain = new PurchaseOrderDomain(owner);
            var result = await orderDomain.CalcPromoOnline(orderEntity);
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [Route("local/calcpromo")]
        [HttpPost]
        public async Task<IHttpActionResult> ClacPromoLocal(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            PurchaseOrderViewModel result = new PurchaseOrderViewModel();
            await Task.Factory.StartNew(() =>
            {
                var orderDomain = new PMCPurchaseOrderDomain();
                result = orderDomain.GetPerformaInvoicePreview(orderEntity);
            });
            return Ok(result);
        }

    }
}
