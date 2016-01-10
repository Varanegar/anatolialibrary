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
    public class IncomplatePurchaseOrderController : BaseApiController
    {
        [Authorize(Roles = "User")]
        [Route("")]
        public async Task<IHttpActionResult> GetByCustomerId(string privateOwnerId, string customerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new IncompletePurchaseOrderDomain(owner);
                var result = await basketDomain.GetAllByCustomerId(customerId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveIncompletePurchaseOrder(string privateOwnerId, List<IncompletePurchaseOrderViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new IncompletePurchaseOrderDomain(owner);
                var result = await basketDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("clear")]
        public async Task<IHttpActionResult> ClearIncompletePurchaseOrder(string privateOwnerId, List<IncompletePurchaseOrderViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new IncompletePurchaseOrderDomain(owner);
                var result = await basketDomain.Clear(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "User")]
        [Route("lineitem/save")]
        public async Task<IHttpActionResult> SaveIncompletePurchaseOrderItem(string privateOwnerId, List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new IncompletePurchaseOrderLineItemDomain(owner);
                var result = await basketDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("lineitem/change")]
        public async Task<IHttpActionResult> ChangeIncompletePurchaseOrderItem(string privateOwnerId, List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new IncompletePurchaseOrderLineItemDomain(owner);
                var result = await basketDomain.ChangeAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("lineitem/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteIncompletePurchaseOrderitem(string privateOwnerId, List<IncompletePurchaseOrderLineItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new IncompletePurchaseOrderLineItemDomain(owner);
                var result = await basketDomain.Delete(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
    }
}
