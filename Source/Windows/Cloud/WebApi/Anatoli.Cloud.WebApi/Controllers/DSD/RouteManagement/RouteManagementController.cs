using Anatoli.Business.Domain;
using Anatoli.Business.Domain.Route;
using Anatoli.Business.Proxy.Concretes.ProductConcretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
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
    [RoutePrefix("api/dsd/route")]
    public class RouteManagementController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("areas")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreas([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = await new RegionAreaDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync(p => p.RegionAreaParentId == data.uniqueId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

    }
}
