using Anatoli.Business.Domain;
using Anatoli.Business.Proxy.Concretes.ProductConcretes;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/base/manufacture")]
    public class ManufactureController : AnatoliApiController
    {
        #region Manufacture
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("manufactures")]
        [HttpPost]
        public async Task<IHttpActionResult> GetManufactures()
        {
            try
            {
                var manufactureDomain = new ManufactureDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await manufactureDomain.GetAllAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("manufactures/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetManufactures([FromBody]BaseRequestModel data)
        {
            try
            {
                var manufactureDomain = new ManufactureDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = DateTime.Parse(data.dateAfter);
                var result = await manufactureDomain.GetAllChangedAfterAsync(validDate);

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
        public async Task<IHttpActionResult> SaveManufactures([FromBody]GeneralRequestModel data)
        {
            var manufactureDomain = new ManufactureDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
            await manufactureDomain.PublishAsync(new ManufactureProxy().ReverseConvert(data.manufactureData));
            return Ok(data.manufactureData);
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedManufactures([FromBody]GeneralRequestModel data)
        {
            var manufactureDomain = new ManufactureDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
            await manufactureDomain.CheckDeletedAsync(data.manufactureData);
            return Ok(data.manufactureData);
        }
        #endregion
    }
}
