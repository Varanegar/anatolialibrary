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
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.Concretes.BaseConcretes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/fiscalyear")]
    public class FiscalYearController : AnatoliApiController
    {
        //[Authorize(Roles = "AuthorizedApp, User")]
        [Route("fiscalyears")]
        public async Task<IHttpActionResult> GetFiscalYears()
        {
            try
            {
                var result = await new FiscalYearDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();
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
        public async Task<IHttpActionResult> SaveFiscalYears([FromBody]GeneralRequestModel data)
        {
            try
            {
                await new FiscalYearDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).PublishAsync(new FiscalYearProxy().ReverseConvert(data.fiscalYearData));

                return Ok(data.fiscalYearData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

    }
}
