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
    public class CustomerController : ApiController
    {
        [Authorize(Roles = "User")]
        [Route("customerbaskets")]
        public async Task<IHttpActionResult> GetCustomerBasket(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new BasketDomain(owner);
            var result = await basketDomain.GetBasketByCustomerId(id);

            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveBasket(string privateOwnerId, List<BasketViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new BasketDomain(owner);
            await basketDomain.PublishAsync(data);
            return Ok();
        }


        [Authorize(Roles = "User")]
        [Route("basketitem/save")]
        public async Task<IHttpActionResult> SaveBasketItem(string privateOwnerId, List<BasketItemViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new BasketItemDomain(owner);
            await basketDomain.PublishAsync(data);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("basketitem/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBasketitem(string privateOwnerId, List<BasketItemViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new BasketItemDomain(owner);
            await basketDomain.Delete(data);
            return Ok();
        }
    }
}
