using Anatoli.Business.Domain;
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
    public class CustomerController : BaseApiController
    {
        [Authorize(Roles = "User")]
        [Route("customerbaskets/bycustomer")]
        public async Task<IHttpActionResult> GetCustomerBasketByCustomerId(string privateOwnerId, string customerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketDomain(owner);
                var result = await basketDomain.GetBasketByCustomerId(customerId);

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
        public async Task<IHttpActionResult> GetCustomerBasketByBasketId(string privateOwnerId, string basketId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketDomain(owner);
                var result = await basketDomain.GetBasketByBasketId(basketId);

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
        public async Task<IHttpActionResult> SaveBasket(string privateOwnerId, List<BasketViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketDomain(owner);
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
        [Route("delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBasket(string privateOwnerId, List<BasketViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketDomain(owner);
                var result = await basketDomain.Delete(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "User")]
        [Route("basketitem/save")]
        public async Task<IHttpActionResult> SaveBasketItem(string privateOwnerId, List<BasketItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketItemDomain(owner);
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
        [Route("basketitem/change")]
        public async Task<IHttpActionResult> ChangeBasketItem(string privateOwnerId, List<BasketItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketItemDomain(owner);
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
        [Route("basketitem/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBasketitem(string privateOwnerId, List<BasketItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketItemDomain(owner);
                var result = await basketDomain.Delete(data);
                return Ok(result);
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
        public async Task<IHttpActionResult> GetBasketitemByIds(string privateOwnerId, List<BasketItemViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var basketDomain = new BasketItemDomain(owner);
                var result = await basketDomain.GetByIds(data);
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
