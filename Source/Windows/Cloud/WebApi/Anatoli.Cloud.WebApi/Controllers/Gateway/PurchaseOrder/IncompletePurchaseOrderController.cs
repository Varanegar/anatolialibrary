using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.Concretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
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
    public class IncomplatePurchaseOrderController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByCustomerId([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var businessDomain = new IncompletePurchaseOrderDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await businessDomain.GetAllByCustomerId(data.customerId);

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
        [HttpPost]
        public async Task<IHttpActionResult> SaveIncompletePurchaseOrder([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var businessDomain = new IncompletePurchaseOrderDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await businessDomain.PublishAsync(new IncompletePurchaseOrderProxy().ReverseConvert(data.incompletePurchaseOrderData));
                return Ok(data.incompletePurchaseOrderData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("clear")]
        [HttpPost]
        public async Task<IHttpActionResult> ClearIncompletePurchaseOrder([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var businessDomain = new IncompletePurchaseOrderDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await businessDomain.Clear(new IncompletePurchaseOrderProxy().ReverseConvert(data.incompletePurchaseOrderData));
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
        [HttpPost]
        public async Task<IHttpActionResult> SaveIncompletePurchaseOrderItem([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var businessDomain = new IncompletePurchaseOrderLineItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await businessDomain.PublishAsync(new IncompletePurchaseOrderLineItemProxy().ReverseConvert(data.incompletePurchaseOrderLineItemData));
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
        [HttpPost]
        public async Task<IHttpActionResult> ChangeIncompletePurchaseOrderItem([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var businessDomain = new IncompletePurchaseOrderLineItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await businessDomain.ChangeAsync(new IncompletePurchaseOrderLineItemProxy().ReverseConvert(data.incompletePurchaseOrderLineItemData));
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
        public async Task<IHttpActionResult> DeleteIncompletePurchaseOrderitem([FromBody] PurchaseOrderRequestModel data)
        {
            try
            {
                var businessDomain = new IncompletePurchaseOrderLineItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await businessDomain.DeleteAsync(data.incompletePurchaseOrderLineItemData);
                return Ok(data.incompletePurchaseOrderLineItemData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
    }
}
