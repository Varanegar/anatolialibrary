using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.ProductConcretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/region")]
    public class RegionController : AnatoliApiController
    {
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("cityregions")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCityRegion()
        {
            try
            {
                var result = await new CityRegionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("cityregions/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCityRegion([FromBody]BaseRequestModel data)
        {
            try
            {
                var cityRegionDomain = new CityRegionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await cityRegionDomain.GetAllChangedAfterAsync(validDate);

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
        public async Task<IHttpActionResult> SaveCityRegionInfo([FromBody]GeneralRequestModel data)
        {
            try
            {
                var cityRegionDomain = new CityRegionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var saveData = new CityRegionProxy().ReverseConvert(data.cityRegionData);
                await cityRegionDomain.PublishAsync(saveData);
                return Ok(data.cityRegionData);
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
        public async Task<IHttpActionResult> CheckDeletedCityRegionInfo([FromBody]GeneralRequestModel data)
        {
            try
            {
                var cityRegionDomain = new CityRegionDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await cityRegionDomain.CheckDeletedAsync(data.cityRegionData);
                return Ok(data.cityRegionData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
    }
}
