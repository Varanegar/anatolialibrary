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
using Excel = Microsoft.Office.Interop.Excel;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.Report
{
    [RoutePrefix("api/dsd/financereport")]
    public class FinanceReportController : AnatoliApiController
    {

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(true);

        }

        [Authorize(Roles = "User")]
        [Route("ldfinrep")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadFinanceReport([FromBody]FinanceReportRequestModel data)
        {
            try
            {
                var result = new List<DMCPolyViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCFinanceReportDomain();
                    result = service.LoadFinanceReport(data.ToDMCFinanceReportFilterViewModel());
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
        [Route("ldfinvalrep")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadFinanceValueReport([FromBody]FinanceReportRequestModel filter)
        {
            try
            {
                var result = new List<PointViewModel>();

                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCFinanceReportDomain();

                    var dmcpoints = service.LoadFinanceValueReport(filter.ToDMCFinanceValueReportFilterViewModel());
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
        public async Task<IHttpActionResult> LoadFinanceReportCustomer([FromBody]FinanceReportRequestModel data)
        {
            try
            {
                var result = new List<DMCPointViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCFinanceReportDomain();
                    result = service.LoadFinanceReportCustomer(data.ClientId, data.AreaIds);

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
        public async Task<IHttpActionResult> RemoveFinanceReportCache([FromBody]FinanceReportRequestModel data)
        {
            try
            {
                var result = true;
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCFinanceReportDomain();
                    result = service.RemoveFinanceReportCache(data.ClientId);

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
