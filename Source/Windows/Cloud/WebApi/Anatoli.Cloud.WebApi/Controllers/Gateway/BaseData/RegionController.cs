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
    public class RegionController : ApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("cityregions")]
        public async Task<IHttpActionResult> GetCityRegion(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var cityRegionDomain = new CityRegionDomain(owner);
            var result = await cityRegionDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("cityregions/after")]
        public async Task<IHttpActionResult> GetCityRegion(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var cityRegionDomain = new CityRegionDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await cityRegionDomain.GetAllChangedAfter(validDate);

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveCityRegionInfo(string privateOwnerId, List<CityRegionViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var cityRegionDomain = new CityRegionDomain(owner);
            await cityRegionDomain.PublishAsync(data);
            return Ok();
        }        

    }
}
