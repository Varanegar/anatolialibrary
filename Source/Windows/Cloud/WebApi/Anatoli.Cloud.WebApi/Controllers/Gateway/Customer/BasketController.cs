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
        [Authorize(Roles = "AuthorizedApp")]
        [Route("customerbaskets")]
        public async Task<IHttpActionResult> GetCustomerBasket(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new BasketDomain(owner);
            var result = await basketDomain.GetBasketByCustomerId(id);

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveBasket(string privateOwnerId, List<BasketViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var basketDomain = new BasketDomain(owner);
            await basketDomain.PublishAsync(data);
            return Ok();
        }        

    }
}
