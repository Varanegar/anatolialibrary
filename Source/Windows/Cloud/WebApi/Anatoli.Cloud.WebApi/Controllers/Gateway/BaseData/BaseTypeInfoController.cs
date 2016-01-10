using Anatoli.Cloud.WebApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Anatoli.Cloud.WebApi.Models;
using System.Security.Claims;
using Anatoli.Business.Domain;
using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/basedata")]
    public class BaseTypeInfoController : BaseApiController
    {
        public class RequestModel
        {
            public string privateOwnerId { get; set; }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("basedatas")]
        public async Task<IHttpActionResult> GetBaseTypes(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var baseTypeDomain = new BaseTypeDomain(owner);
                var result = await baseTypeDomain.GetAll();

                return Ok(result);
            }
            catch(Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("basedatas/after")]
        public async Task<IHttpActionResult> GetBaseTypes(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var baseTypeDomain = new BaseTypeDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await baseTypeDomain.GetAllChangedAfter(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("basedatas/save")]
        public async Task<IHttpActionResult> SaveBaseTypes(string privateOwnerId, List<BaseTypeViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var baseTypeDomain = new BaseTypeDomain(owner);
                var result = await baseTypeDomain.PublishAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        #region Stock Reorder Calc Type List
        [Authorize(Roles = "User")]
        [Route("reordercalctypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetReorderCalcTypes([FromBody] RequestModel data)
        {
            try
            {
                var owner = Guid.Parse(data.privateOwnerId);
                var baseTypeDomain = new ReorderCalcTypeDomain(owner);
                var result = await baseTypeDomain.GetAll();

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
        [Authorize(Roles = "User")]
        [Route("stocktypes")]
        public async Task<IHttpActionResult> GetStockTypes(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var baseTypeDomain = new StockTypeDomain(owner);
                var result = await baseTypeDomain.GetAll();

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
        [Authorize(Roles = "User")]
        [Route("stockrequesttypes")]
        public async Task<IHttpActionResult> GetStockRequestTypes(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var stockDomain = new StockProductDomain(owner);
                var result = await stockDomain.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stockrequesttype/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStockRequestTypes(string privateOwnerId, List<StockProductRequestTypeViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var businessDomain = new StockProductRequestTypeDomain(owner);
                var result = await businessDomain.PublishAsync(data);
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
