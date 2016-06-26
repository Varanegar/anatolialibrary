using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.DMC.Business.Domain;
using Anatoli.SDS.Business.Domain.Gis;
using Anatoli.ViewModels.CommonModels;
using Anatoli.ViewModels.RequestModel;
using System.Threading.Tasks;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.Report
{
    [RoutePrefix("api/dsd/report")]
    public class ProductReportFilterController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("ldcmblst")]
        [HttpPost]
        public async Task<IHttpActionResult> GetComboData([FromBody]ReportDataRequestModel filter)
        {
            try
            {
                var result = new List<SelectListItemViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new SDSProductReportFilterDomain();
                    result = service.GetComboData(filter.tblName, filter.textName, filter.valueName);

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
        [Route("ldatcmplst")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAutoCompleteData([FromBody]ReportDataRequestModel filter)
        {
            try
            {
                var result = new List<SelectListItemViewModel>();
                if (filter.searchTrem != "")
                await Task.Factory.StartNew(() =>
                {
                    var service = new SDSProductReportFilterDomain();
                    result = service.GetAutoCompleteData(filter.tblName, filter.textName, filter.valueName, filter.searchTrem);
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
        [Route("ldrptlst")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadReportList([FromBody]ReportDataRequestModel filter)
        {
            try
            {
                var result = new List<ReportListViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCGisReportDomain();
                    result = service.LoadReportList(filter.reportName);

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
