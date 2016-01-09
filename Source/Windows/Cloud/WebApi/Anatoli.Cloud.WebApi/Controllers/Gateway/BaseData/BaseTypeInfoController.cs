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
            var owner = Guid.Parse(privateOwnerId);
            var baseTypeDomain = new BaseTypeDomain(owner);
            var result = await baseTypeDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("basedatas/after")]
        public async Task<IHttpActionResult> GetBaseTypes(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var baseTypeDomain = new BaseTypeDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await baseTypeDomain.GetAllChangedAfter(validDate);

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("basedatas/save")]
        public async Task<IHttpActionResult> SaveBaseTypes(string privateOwnerId, List<BaseTypeViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var baseTypeDomain = new BaseTypeDomain(owner);
            await baseTypeDomain.PublishAsync(data);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [Route("reordercalctypes")]
        [HttpPost]
        public async Task<IHttpActionResult> GetReorderCalcTypes([FromBody] RequestModel data)
        {
            var owner = Guid.Parse(data.privateOwnerId);
            var baseTypeDomain = new ReorderCalcTypeDomain(owner);
            var result = await baseTypeDomain.GetAll();

            result.Add(new ReorderCalcTypeViewModel { UniqueId = Guid.Empty, ReorderTypeName = string.Empty });
            
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [Route("stocktypes")]
        public async Task<IHttpActionResult> GetStockTypes(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var baseTypeDomain = new StockTypeDomain(owner);
            var result = await baseTypeDomain.GetAll();

            return Ok(result);
        }
    }
}
