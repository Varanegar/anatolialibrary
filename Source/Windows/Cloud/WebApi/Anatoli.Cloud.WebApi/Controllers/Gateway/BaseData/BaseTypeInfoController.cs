using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using System.Collections.Generic;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.BaseConcretes;
using Anatoli.Business.Proxy.Concretes.StockProductRequestTypeConcretes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/basedata")]
    public class BaseTypeInfoController : AnatoliApiController
    {
        #region Base Type
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("basedatas")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBaseTypes()
        {
            try
            {
                var result = await new BaseTypeDomain(OwnerKey, OwnerKey, OwnerKey).GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("basedatas/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBaseTypes([FromBody]BaseRequestModel data)
        {
            try
            {
                var validDate = GetDateFromString(data.dateAfter);
                var result = await new BaseTypeDomain(OwnerKey, OwnerKey, OwnerKey).GetAllChangedAfterAsync(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("basedatas/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveBaseTypes([FromBody]GeneralRequestModel data)
        {
            try
            {
                await new BaseTypeDomain(OwnerKey, OwnerKey, OwnerKey).PublishAsync(new BaseTypeProxy().ReverseConvert(data.baseTypeData));
                return Ok(data.baseTypeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("basedatas/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedBaseTypes([FromBody]GeneralRequestModel data)
        {
            try
            {
                await new BaseTypeDomain(OwnerKey, OwnerKey, OwnerKey).CheckDeletedAsync(data.baseTypeData);
                return Ok(data.baseTypeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }
        #endregion

        #region Stock Reorder Calc Type List
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("reordercalctypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetReorderCalcTypes([FromBody] BaseRequestModel data)
        {
            try
            {
                var baseTypeDomain = new ReorderCalcTypeDomain(OwnerKey, OwnerKey, OwnerKey);
                var result = await baseTypeDomain.GetAllAsync();

                result.Add(new ReorderCalcTypeViewModel { UniqueId = Guid.Empty, ReorderTypeName = string.Empty });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Stock Stock Type List
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("stocktypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockTypes()
        {
            try
            {
                var baseTypeDomain = new StockTypeDomain(OwnerKey, OwnerKey, OwnerKey);
                var result = await baseTypeDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Stock Requests Type List
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("stockrequesttypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStockRequestTypes()
        {
            try
            {
                var domain = new StockProductRequestTypeDomain(OwnerKey, OwnerKey, OwnerKey);
                var result = await domain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("stockrequesttype/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockRequestTypes([FromBody] GeneralRequestModel data)
        {
            try
            {
                var businessDomain = new StockProductRequestTypeDomain(OwnerKey, OwnerKey, OwnerKey);
                await businessDomain.PublishAsync(new StockProductRequestTypeProxy().ReverseConvert(data.baseStockProductRequestType));
                return Ok(data.baseStockProductRequestType);
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
