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

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/fiscalyear")]
    public class FiscalYearController : BaseApiController
    {
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("fiscalyears")]
        public async Task<IHttpActionResult> GetFiscalYears(string privateOwnerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var fiscalYearDomain = new FiscalYearDomain(owner);
                var result = await fiscalYearDomain.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("fiscalyears/after")]
        public async Task<IHttpActionResult> GetFiscalYears(string privateOwnerId, string dateAfter)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var fiscalYearDomain = new FiscalYearDomain(owner);
                var validDate = DateTime.Parse(dateAfter);
                var result = await fiscalYearDomain.GetAllChangedAfter(validDate);

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
        public async Task<IHttpActionResult> SaveFiscalYears(string privateOwnerId, List<FiscalYearViewModel> data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var fiscalYearDomain = new FiscalYearDomain(owner);
                var result = await fiscalYearDomain.PublishAsync(data);
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
