using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Cloud.WebApi.Handler.AutoMapper;
using Anatoli.DMC.Business.Domain;
using Anatoli.ViewModels;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.Customer
{
    [RoutePrefix("api/dsd/customer")]
    public class DSDCustomerController :  AnatoliApiController
    {

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(true);
            
        }
        
        [Authorize(Roles = "User")]
        [Route("ldsrch")]
        [HttpPost]

        public async Task<IHttpActionResult> LoadCustomerBySearchTerm([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = new List<CustomerComboViewModel>();
                if (data.searchTerm != "")
                await Task.Factory.StartNew(() =>
                {
                    var customerService = new DMCCustomerDomain();
                    var dmcresult = customerService.LoadCustomerBySearchTerm(data.searchTerm);
                    result = dmcresult.Select(x => x.ToComboViewModel()).ToList();
                });
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
