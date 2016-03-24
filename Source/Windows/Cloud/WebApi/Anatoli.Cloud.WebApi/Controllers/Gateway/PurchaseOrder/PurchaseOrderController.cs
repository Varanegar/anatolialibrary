using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.Concretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.PMC.Business.Domain.PurchaseOrder;
using Anatoli.ViewModels;
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
    public class PurchaseOrderController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrder([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var orderDomain = new PurchaseOrderDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await orderDomain.PublishOrderOnline(new PurchaseOrderProxy().ReverseConvert(data.orderEntity));
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "InternalCommunication")]
        [Route("local/create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrderLocal([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                PurchaseOrderViewModel result = new PurchaseOrderViewModel();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderDomain();
                    result = orderDomain.Publish(data.orderEntity);
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
        [HttpPost]
        public async Task<IHttpActionResult> ClacPromo([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var orderDomain = new PurchaseOrderDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await orderDomain.CalcPromoOnline(data.orderEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "InternalCommunication")]
        [Route("local/calcpromo")]
        [HttpPost]
        public async Task<IHttpActionResult> ClacPromoLocal([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                PurchaseOrderViewModel result = new PurchaseOrderViewModel();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderDomain();
                    result = orderDomain.GetPerformaInvoicePreview(data.orderEntity);
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
        [Route("bycustomerid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPurchaseOrderOnlineByCustomerId([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var orderDomain = new PurchaseOrderDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await orderDomain.GetAllByCustomerIdOnLine(data.customerId);
                result.ForEach(item =>
                {
                    item.ApplicationOwnerId = OwnerKey;
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "InternalCommunication")]
        [Route("bycustomerid/local")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPurchaseOrderLocalByCustomerId([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var result = new List<PurchaseOrderViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderDomain();
                    result = orderDomain.GetAllByCustomerId(data.customerId, null, data.centerId.ToString());
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("lineitem")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPurchaseOrderLineItemOnline([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var orderDomain = new PurchaseOrderLineItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await orderDomain.GetAllByPOIdOnLine(data.poId);
                result.ForEach(item =>
                {
                    item.ApplicationOwnerId = OwnerKey;
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "InternalCommunication")]
        [Route("lineitem/local")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPurchaseOrderLineItemLocal([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var result = new List<PurchaseOrderLineItemViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderLineItemDomain();
                    result = orderDomain.GetAllByOrderId(data.poId, data.centerId);
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("statushistory")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPurchaseOrderStatusHistoryOnline([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var orderDomain = new PurchaseOrderStatusHistoryDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await orderDomain.GetAllByPOIdOnLine(data.poId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "InternalCommunication")]
        [Route("statushistory/local")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPurchaseOrderStatusHistoryLocal([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var result = new List<PurchaseOrderStatusHistoryViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var orderDomain = new PMCPurchaseOrderStatusHistoryDomain();
                    result = orderDomain.GetAllByOrderId(data.poId, data.centerId);
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
