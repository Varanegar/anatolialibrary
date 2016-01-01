using Anatoli.Business.Domain;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/supplier")]
    public class SupplierController : ApiController
    {
        #region Products
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("suppliers")]
        public async Task<IHttpActionResult> GetSuppliers(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var supplierDomain = new SupplierDomain(owner);
            var result = await supplierDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("suppliers/after")]
        public async Task<IHttpActionResult> GetSuppliers(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var supplierDomain = new SupplierDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await supplierDomain.GetAllChangedAfter(validDate);

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveSuppliers(string privateOwnerId, List<SupplierViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var supplierDomain = new SupplierDomain(owner);
            await supplierDomain.PublishAsync(data);
            return Ok();
        }
        #endregion
    }
}
