using Anatoli.Business.Domain;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/incompletepurchaseorder")]
    public class IncomplatePurchaseOrderController : ApiController
    {
        [Authorize(Roles = "User")]
        [Route("")]
        public async Task<IHttpActionResult> GetByCustomerId(string privateOwnerId, string customerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new IncompletePurchaseOrderDomain(owner);
            var result = await basketDomain.GetAllByCustomerId(customerId);

            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveIncompletePurchaseOrder(string privateOwnerId, List<IncompletePurchaseOrderViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new IncompletePurchaseOrderDomain(owner);
            await basketDomain.PublishAsync(data);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("clear")]
        public async Task<IHttpActionResult> ClearIncompletePurchaseOrder(string privateOwnerId, List<IncompletePurchaseOrderViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new IncompletePurchaseOrderDomain(owner);
            await basketDomain.Clear(data);
            return Ok();
        }


        [Authorize(Roles = "User")]
        [Route("lineitem/save")]
        public async Task<IHttpActionResult> SaveIncompletePurchaseOrderItem(string privateOwnerId, List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new IncompletePurchaseOrderLineItemDomain(owner);
            await basketDomain.PublishAsync(data);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("lineitem/change")]
        public async Task<IHttpActionResult> ChangeIncompletePurchaseOrderItem(string privateOwnerId, List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new IncompletePurchaseOrderLineItemDomain(owner);
            await basketDomain.ChangeAsync(data);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("lineitem/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteIncompletePurchaseOrderitem(string privateOwnerId, List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new IncompletePurchaseOrderLineItemDomain(owner);
            await basketDomain.Delete(data);
            return Ok();
        }
    }
}
