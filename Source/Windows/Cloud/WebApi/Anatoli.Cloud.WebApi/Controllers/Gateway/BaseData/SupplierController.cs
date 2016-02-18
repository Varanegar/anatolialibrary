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
    public class SupplierController : BaseApiController
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
