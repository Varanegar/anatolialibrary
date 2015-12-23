using Anatoli.Business.Domain;
using Anatoli.Cloud.Gateway.Business.Manufacture;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base")]
    public class ManufactureController : ApiController
    {
        #region Products
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("manufacture")]
        public async Task<IHttpActionResult> GetProducts(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var manufactureDomain = new ManufactureDomain(owner);
            var result = await manufactureDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("manufacture/save")]
        public async Task<IHttpActionResult> SaveManufactures(string privateOwnerId, List<ManufactureViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var manufactureDomain = new ManufactureDomain(owner);
            await manufactureDomain.PublishAsync(data);
            return Ok();
        }
        #endregion
    }
}
