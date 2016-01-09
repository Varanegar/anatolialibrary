using Anatoli.Business.Domain;
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
    public class RegionController : BaseApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("cityregions")]
        public async Task<IHttpActionResult> GetCityRegion(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var cityRegionDomain = new CityRegionDomain(owner);
                var result = await cityRegionDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("cityregions/after")]
        public async Task<IHttpActionResult> GetCityRegion(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var cityRegionDomain = new CityRegionDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await cityRegionDomain.GetAllChangedAfter(validDate);

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
        public async Task<IHttpActionResult> SaveCityRegionInfo(string privateOwnerId, List<CityRegionViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var cityRegionDomain = new CityRegionDomain(owner);
                var result = await cityRegionDomain.PublishAsync(data);
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
