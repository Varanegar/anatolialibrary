using Anatoli.Business.Domain;
using Anatoli.ViewModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/customer")]
    public class BasketController : ApiController
    {
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customers")]
        public async Task<IHttpActionResult> GetCustomerById(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var customerDomain = new CustomerDomain(owner);
            var result = await customerDomain.GetCustomerById(id);

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveCustomer(string privateOwnerId, CustomerViewModel data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var customerDomain = new CustomerDomain(owner);
            List<CustomerViewModel> dataList = new List<CustomerViewModel>();
            dataList.Add(data);
            await customerDomain.PublishAsync(dataList);
            return Ok();
        }        

    }
}
