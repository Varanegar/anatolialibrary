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
    public class PurchaseOrderController : BaseApiController
    {
        [Authorize(Roles = "User")]
        [Route("create")]
        public async Task<IHttpActionResult> CreateOrder(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var orderDomain = new PurchaseOrderDomain(owner);
                var result = await orderDomain.PublishOrderOnline(orderEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("local/create")]
        public async Task<IHttpActionResult> CreateOrderLocal(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            try
            {
                PurchaseOrderViewModel result = new PurchaseOrderViewModel();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderDomain();
                    result = orderDomain.Publish(orderEntity);
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("calcpromo")]
        public async Task<IHttpActionResult> ClacPromo(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var orderDomain = new PurchaseOrderDomain(owner);
                var result = await orderDomain.CalcPromoOnline(orderEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("local/calcpromo")]
        [HttpPost]
        public async Task<IHttpActionResult> ClacPromoLocal(string privateOwnerId, PurchaseOrderViewModel orderEntity)
        {
            try
            {
                PurchaseOrderViewModel result = new PurchaseOrderViewModel();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderDomain();
                    result = orderDomain.GetPerformaInvoicePreview(orderEntity);
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("bycustomerid/online")]
        public async Task<IHttpActionResult> GetPurchaseOrderOnlineByCustomerId(string privateOwnerId, string customerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var orderDomain = new PurchaseOrderDomain(owner);
                var result = await orderDomain.GetAllByCustomerIdOnLine(customerId);
                result.ForEach(item =>
                {
                    item.PrivateOwnerId = owner;
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("bycustomerid/local")]
        public async Task<IHttpActionResult> GetPurchaseOrderLocalByCustomerId(string customerId, int centerId)
        {
            try
            {
                var result = new List<PurchaseOrderViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderDomain();
                    result = orderDomain.GetAllByCustomerId(customerId, null, centerId);
                });
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
