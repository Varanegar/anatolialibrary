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
        [Route("areas/byparentid")]
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

        [Authorize(Roles = "User")]
        [Route("areas/level1")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaLevel1s([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = await new RegionAreaDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAreaLevel1();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("area/haspoint")]
        [HttpPost]
        public async Task<IHttpActionResult> HasRegionAreaPoint([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                bool result = false;
                await Task.Factory.StartNew(() =>
                {

                    result = new RegionAreaPointDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).HasRegionAreaPoint(data.regionAreaId);
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("area/paths")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaPath([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = await new RegionAreaDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAreaPathById(data.regionAreaId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("area/selectedcustomers")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaSelectedCustomers([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = await new RegionAreaDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetRegionAreaSelectedCustomers(data.regionAreaId, true);

                //return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("area/notselectedcustomers")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaNotSelectedCustomers([FromBody]RegionAreaRequestModel data)
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
        [Route("area/customer/save")]
        [HttpPost]
        public async Task<IHttpActionResult> AddRegionAreaCustomers([FromBody]RegionAreaRequestModel data)
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
        [Route("area/customer/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRegionAreaCustomers([FromBody]RegionAreaRequestModel data)
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
        [Route("area/point/save")]
        [HttpPost]
        public async Task<IHttpActionResult> AddRegionAreaPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/point/delete")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveRegionAreaPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/points")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/parentpoints")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaParentPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/sibilingpoints")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaSibilingPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/childpoints")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionChildAreaPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/customerpoints")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaCustomerPoints([FromBody]RegionAreaRequestModel data)
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
        [Route("area/leafcustomerpoints")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaLeafCustomerPoints([FromBody]RegionAreaRequestModel data)
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
