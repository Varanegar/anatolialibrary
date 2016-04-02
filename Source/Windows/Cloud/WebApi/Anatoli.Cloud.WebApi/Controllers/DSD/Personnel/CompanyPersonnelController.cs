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
    [RoutePrefix("api/dsd/personnel")]
    public class CompanyPersonnelController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("byareas")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPersonGroupByArea([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("byorgcharts")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPersonByGroup([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("events")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPersonEvents([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("activities")]
        [HttpPost]
        public async Task<IHttpActionResult> GetPersonActivities([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
    }
}
