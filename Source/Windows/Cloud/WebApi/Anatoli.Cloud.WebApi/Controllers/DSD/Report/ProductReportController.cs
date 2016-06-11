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
        [Route("expprdrepexcl")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportProductReportToExcel([FromBody]ProductReportRequestModel data)
        {
            try
            {
                var result = new List<DMCPolyViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCProductReportDomain();
                    result = service.LoadProductReport(data.ToDMCProductReportFilterViewModel());
                });

                var excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = true;

                // Create a new, empty workbook and add it to the collection returned 
                var oWB =  excelApp.Workbooks.Add();

                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

                workSheet.Cells[1, "A"] = "Title";
                workSheet.Cells[1, "B"] = "Data";

                var row = 1;
                foreach (var item in result)
                {
                    row++;
                    workSheet.Cells[row, "A"] = item.Desc;
                    workSheet.Cells[row, "B"] = "";
                }

                ((Excel.Range)workSheet.Columns[1]).AutoFit();
                ((Excel.Range)workSheet.Columns[2]).AutoFit();

                oWB.SaveAs("c:\\test\\test505.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
        false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();

                return Ok(true);
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
