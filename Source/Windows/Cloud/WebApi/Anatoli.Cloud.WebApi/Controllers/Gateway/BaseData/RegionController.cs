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
        /// <summary>
        /// Todo: remove it after your testing
        /// </summary>
        /// <returns></returns>
        //[Route("cityregionsTest")]
        //public async Task<IHttpActionResult> GetCityRegionTest()
        //{
        //    try
        //    {
        //        var result = await new CityRegionDomain(new DataAccess.Models.OwnerInfo
        //        {
        //            ApplicationOwnerKey = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
        //            DataOwnerKey = Guid.Parse("79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240"),
        //            DataOwnerCenterKey = Guid.Parse("3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C")
        //        }).GetAllAsync<CityRegionViewModel>();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex, "Web API Call Error");

        //        return GetErrorResult(ex);
        //    }
        //}

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
        [Route("cityregions/compress")]
        [GzipCompression]
        [HttpPost]
        public async Task<IHttpActionResult> GetCityRegionCompress()
        {
            return await GetCityRegion();
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("cityregions/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCityRegion([FromBody]BaseRequestModel data)
        {
            try
            {
                var cityRegionDomain = new CityRegionDomain(OwnerInfo);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await cityRegionDomain.GetAllChangedAfterAsync<CityRegionViewModel>(validDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("cityregions/compress/after")]
        [GzipCompression]
        [HttpPost]
        public async Task<IHttpActionResult> GetCityRegionCompress([FromBody]BaseRequestModel data)
        {
            return await GetCityRegion(data);
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
