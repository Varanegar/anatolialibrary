using Anatoli.Business.Domain;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.ProductConcretes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/supplier")]
    public class SupplierController : AnatoliApiController
    {
        #region Supplier
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("suppliers")]
        [HttpPost]
        public async Task<IHttpActionResult> GetSuppliers()
        {
            try
            {
                var supplierDomain = new SupplierDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await supplierDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("filterSuppliers")]
        [HttpPost]
        public async Task<IHttpActionResult> FilterSuppliers([FromBody]BaseRequestModel data)
        {
            try
            {
                data.searchTerm = data.searchTerm.Replace("ی", "ي").Replace("ک", "ك");
                var model = await new SupplierDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync(p => p.SupplierName.Contains(data.searchTerm));

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
        [HttpPost]
        public async Task<IHttpActionResult> GetSuppliers([FromBody]BaseRequestModel data)
        {
            try
            {
                var supplierDomain = new SupplierDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await supplierDomain.GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveSuppliers([FromBody]GeneralRequestModel data)
        {
            try
            {
                var supplierDomain = new SupplierDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await supplierDomain.PublishAsync(new SupplierProxy().ReverseConvert(data.supplierData));
                return Ok(data.supplierData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeleteSuppliers([FromBody]GeneralRequestModel data)
        {
            try
            {
                var supplierDomain = new SupplierDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await supplierDomain.CheckDeletedAsync(data.supplierData);
                return Ok(data.supplierData);
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
