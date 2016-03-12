using Anatoli.Business.Domain;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.ViewModels;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/supplier")]
    public class SupplierController : AnatoliApiController
    {
        #region Supplier
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("suppliers")]
        public async Task<IHttpActionResult> GetSuppliers(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var supplierDomain = new SupplierDomain(owner);
                var result = await supplierDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("filterSuppliers"), HttpPost]
        public async Task<IHttpActionResult> FilterSuppliers([FromBody]RequestModel data)
        {
            try
            {
                data.searchTerm = data.searchTerm.Replace("ی", "ي").Replace("ک", "ك");
                var model = await new SupplierDomain(OwnerKey).FilterSuppliersAsync(data.searchTerm);

                return Ok(model);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("suppliers/after")]
        public async Task<IHttpActionResult> GetSuppliers(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var supplierDomain = new SupplierDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await supplierDomain.GetAllChangedAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveSuppliers(string privateOwnerId, List<SupplierViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var supplierDomain = new SupplierDomain(owner);
                var result = await supplierDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("checkdeleted")]
        public async Task<IHttpActionResult> CheckDeleteSuppliers(string privateOwnerId, List<SupplierViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var supplierDomain = new SupplierDomain(owner);
                var result = await supplierDomain.CheckDeletedAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion
    }
}
