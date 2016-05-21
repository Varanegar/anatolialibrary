using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Cloud.WebApi.Handler.AutoMapper;
using Anatoli.DMC.Business.Domain;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.ViewModels.RequestModel;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.Report
{
    [RoutePrefix("api/dsd/pruductreport")]
    public class ProductReportController : AnatoliApiController
    {

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(true);

        }

        [Authorize(Roles = "User")]
        [Route("ldprdrep")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadProductReport([FromBody]ProductReportRequestModel data)
        {
            try
            {
                var result = new List<DMCPolyViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCProductReportDomain();
                    result = service.LoadProductReport(data.ToDMCProductReportFilterViewModel());
                   

                });

                return Ok(result.Select(x => x.ToViewModel()).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("ldprdvalrep")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadProductValueReport([FromBody]ProductReportRequestModel filter)
        {
            try
            {
                var result = new List<PointViewModel>();

                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCProductReportDomain();

                    var dmcpoints = service.LoadProductValueReport(filter.ToDMCProductValueReportFilterViewModel());
                    result.AddRange(dmcpoints.Select(x  => x.ToViewModel()).ToList());
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
        [Route("ldcust")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadProductReportCustomer([FromBody]ProductReportRequestModel data)
        {
            try
            {
                var result = new List<DMCPointViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCProductReportDomain();
                    result = service.LoadProductReportCustomer(data.ClientId, data.AreaIds);

                });

                return Ok(result.Select(x => x.ToViewModel()).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        [Authorize(Roles = "User")]
        [Route("rmvcch")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveProductReportCache([FromBody]ProductReportRequestModel data)
        {
            try
            {
                var result = true;
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCProductReportDomain();
                    result = service.RemoveProductReportCache(data.ClientId);

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
