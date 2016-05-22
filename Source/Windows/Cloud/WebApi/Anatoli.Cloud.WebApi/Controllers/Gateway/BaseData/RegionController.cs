using System;
using System.Web.Http;
using Anatoli.ViewModels;
using System.Threading.Tasks;
using Anatoli.Business.Domain;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Business.Proxy.ProductConcretes;
using Anatoli.ViewModels.BaseModels;

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
                var result = await new CityRegionDomain(OwnerInfo).GetAllAsync<CityRegionViewModel>();

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
                var cityRegionDomain = new CityRegionDomain(OwnerInfo);

                var validDate = DateTime.Parse(data.dateAfter);

                var result = await cityRegionDomain.GetAllChangedAfterAsync<CityRegionViewModel>(validDate);

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
                var cityRegionDomain = new CityRegionDomain(OwnerInfo);
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
                var cityRegionDomain = new CityRegionDomain(OwnerInfo);
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
