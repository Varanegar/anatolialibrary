using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.Concretes.ProductConcretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/basket")]
    public class BasketController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("customerbaskets/bycustomer")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerBasketByCustomerId([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new BasketDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetBasketByCustomerId(data.customerId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("customerbaskets/bybasket")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerBasketByBasketId([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new BasketDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetByIdAsync(data.basketId);

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
        public async Task<IHttpActionResult> SaveBasket([FromBody]CustomerRequestModel data)
        {
            try
            {
                var basketData = new BasketProxy().ReverseConvert(data.basketData);
                await new BasketDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).PublishAsync(basketData);
                return Ok(data.basketData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBasket([FromBody]CustomerRequestModel data)
        {
            try
            {
                var basketData = new BasketProxy().ReverseConvert(data.basketData);
                await new BasketDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).DeleteAsync(basketData);
                return Ok(basketData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "User")]
        [Route("basketitem/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveBasketItem([FromBody]CustomerRequestModel data)
        {
            try
            {
                var saveData = new BasketItemProxy().ReverseConvert(data.basketItemData);
                await new BasketItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).PublishAsyncByProductId(saveData);
                return Ok(data.basketItemData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("basketitem/change")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangeBasketItem([FromBody]CustomerRequestModel data)
        {
            try
            {
                var saveData = new BasketItemProxy().ReverseConvert(data.basketItemData);
                var result = await new BasketItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).ChangeAsync(saveData);
                return Ok(saveData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("basketitem/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBasketitem([FromBody]CustomerRequestModel data)
        {
            try
            {
                var saveData = new BasketItemProxy().ReverseConvert(data.basketItemData);
                await new BasketItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).DeleteAsyncByProductId(saveData);
                return Ok(saveData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("basketitems")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBasketitemByIds([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new BasketItemDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetByIds(data.basketItemData);
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
