using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.DMC.Business.Domain;
using Anatoli.DMC.ViewModels.Area;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.ViewModels;
using Anatoli.ViewModels.CommonModels;
using Anatoli.ViewModels.RequestModel;
using Anatoli.ViewModels.VnGisModels;

using Anatoli.Cloud.WebApi.Handler.AutoMapper;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.RouteManagement
{
    [RoutePrefix("api/dsd/route")]
    public class RouteManagementController : AnatoliApiController
    {

        [HttpGet]
        [Route("ping")]

        public IHttpActionResult Ping()
        {
            return Ok(true);

        }

        #region RegionArea
        [Authorize(Roles = "User")]
        [Route("ldarealst")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadRegionAreas([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = new List<RegionAreaViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var areaService = new DMCVisitTemplatePathDomain();
                    var dmcresult = areaService.LoadAreaByParentId(data == null ? null : data.regionAreaParentId);
                    result = dmcresult.Select(x => x.ToViewModel()).ToList();
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
        [Route("getareapath")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaPath([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = new List<DMCVisitTemplatePathViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var areaService = new DMCVisitTemplatePathDomain();
                    result = areaService.GetAreaPathById(data.regionAreaId);
                });

                return Ok(result.Select(x => x.ToSelectedItem()));
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }



        [Authorize(Roles = "User")]
        [Route("ldareabylv")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadRegionAreaByLevel([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = new List<SelectListItemViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCVisitTemplatePathDomain();
                    result = service.LoadByLevel(data.regionAreaLevel, data.regionAreaId);

                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        #endregion

        #region regionAreaPoint
        [Authorize(Roles = "User")]
        [Route("svareapnt")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveRegionAreaPoint([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = true;
                await Task.Factory.StartNew(() =>
                {
                    var areaPointService = new DMCRegionAreaPointDomain();

                    var points = data.regionAreaPointDataList.Select(x => x.ToDMCViewModel()).ToList();
                    result = areaPointService.SaveAreaPointList(data.regionAreaId, points);
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        //---------------------------------------
        //  load region point
        //---------------------------------------
        [Authorize(Roles = "User")]
        [Route("ldareapoint")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadAreaPoints([FromBody]RegionAreaRequestModel data)
        {

            try
            {
                var poly = new PolyViewModel();
                await Task.Factory.StartNew(() =>
                {

                    var areaPointService = new DMCRegionAreaPointDomain();
                    var points = areaPointService.LoadAreaPointById(data.regionAreaId);
                    poly.color = "#000000";
                    poly.desc = "";
                    poly.points = points.Select(x => x.ToViewModel()).ToList();
                     
                });

                return Ok(poly);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("ldarealines")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadAreaLines([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var polies = new List<PolyViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var areaPointService = new DMCRegionAreaPointDomain();
                    foreach (var areaId in data.regionAreaIds)
                    {
                        var poly = new PolyViewModel();
                        var points = areaPointService.LoadAreaPointById(areaId);
                        poly.color = "#000000";
                        poly.desc = "";
                        poly.points = points.Select(x => x.ToViewModel()).ToList();
                        polies.Add(poly);
                    }
                });

                return Ok(polies);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        
        [Authorize(Roles = "User")]
        [Route("hsareapoint")]
        [HttpPost]
        public async Task<IHttpActionResult> HasRegionAreaPoint([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                bool result = false;
                await Task.Factory.StartNew(() =>
                {
                    var areaPointService = new DMCRegionAreaPointDomain();
                    var haspoint = areaPointService.HaseAreaPoint(data.regionAreaId);
                    result = haspoint;

                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        //---------------------------------------
        //  load parent limit
        //---------------------------------------
        [Authorize(Roles = "User")]
        [Route("ldareaprntpnt")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadRigenAreaParentPoints([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var poly = new PolyViewModel();
                await Task.Factory.StartNew(() =>
                {
                    var areaService = new DMCVisitTemplatePathDomain();
                    var areaPointService = new DMCRegionAreaPointDomain();
                    var parentid = areaService.GetParentIdById(data.regionAreaId);

                    if (parentid != null)
                    {
                        var parentpoints = areaPointService.LoadAreaPointById(parentid).ToList();
                        if (parentpoints.Any())
                        {
                            parentpoints.Add(parentpoints.ElementAt(0));
                        }
                        poly.points = parentpoints.Select(x => x.ToViewModel()).ToList();
                        poly.color = "#888888";
                        poly.desc = "";
                    }
                });

                return Ok(poly);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }


        [Authorize(Roles = "User")]
        [Route("ldareasblpnt")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadAreaSibilingPoints([FromBody]RegionAreaRequestModel data)
        {

            try
            {
                var siblingpoints = new List<DMCPolyViewModel>();
                await Task.Factory.StartNew(() =>
                {

                    var routeService = new DMCRouteDomain();
                    siblingpoints = routeService.LoadAreaSibilingPoints(data.regionAreaId).ToList();
                });
                
                return Ok(siblingpoints.Select(x => x.ToViewModel()).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        [Authorize(Roles = "User")]
        [Route("ldareachldpnt")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadAreaChildPoints([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var siblingpoints = new List<DMCPolyViewModel>();
                await Task.Factory.StartNew(() =>
                {

                    var routeService = new DMCRouteDomain();
                    siblingpoints = routeService.LoadAreaChildPoints(data.regionAreaId).ToList();
                });

                return Ok(siblingpoints.Select(x => x.ToViewModel()).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }


        }


        [Authorize(Roles = "User")]
        [Route("rmvareapnt")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveAreaPointsByAreaId([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = false;
                await Task.Factory.StartNew(() =>
                {
                    var routeService = new DMCRouteDomain();
                    result = routeService.RemoveAreaPointsByAreaId(data.regionAreaId);
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        
        #endregion

        #region regionAreaCustomer
        [Authorize(Roles = "User")]
        [Route("ldselectedcust")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaSelectedCustomer([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = new List<DMCRegionAreaCustomerViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var customerService = new DMCRegionAreaCustomerDomain();
                    result = customerService.LoadCustomerViewByAreaId(data.regionAreaId, true);
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
        [Route("ldntselectedcust")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRegionAreaNotSelectedCustomer([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = new List<DMCRegionAreaCustomerViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var customerService = new DMCRegionAreaCustomerDomain();
                    result = customerService.LoadCustomerViewByAreaId(data.regionAreaId, false);
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
        [Route("adcusttoarea")]
        [HttpPost]
        public async Task<IHttpActionResult> AddCustomerToRegionArea([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = false;
                await Task.Factory.StartNew(() =>
                {
                    var areaCustomerService = new DMCRegionAreaCustomerDomain();
                    result = areaCustomerService.AddCustomerToRegionArea(data.customerId, data.regionAreaId);
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
        [Route("rmvcustfrmarea")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveCustomerFromRegionArea([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = false;
                await Task.Factory.StartNew(() =>
                {
                    var areaCustomerService = new DMCRegionAreaCustomerDomain();
                    result = areaCustomerService.RemoveCustomerFromRegionArea(data.customerId, data.regionAreaId);
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
        [Route("ldrutcustpnt")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadRouteCustomerPoints([FromBody]RegionAreaRequestModel data)
        {

            try
            {
                var result = new List<DMCPointViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCRouteDomain();
                    result = service.LoadCustomerByRouteId(data.regionAreaId,
                        data.showCustRoute, data.showCustOtherRoute, data.showCustWithOutRoute);
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
        [Route("ldareacustpnt")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadRegionAreaCustomerPoints([FromBody]RegionAreaRequestModel data)
        {

            try
            {
                var result = new List<DMCPointViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCRegionAreaCustomerDomain();
                    result = service.LoadCustomerPointByAreaId(data.regionAreaId);
                });

                return Ok(result.Select(x => x.ToViewModel()).ToList());
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        //------------------------------------------------------------
        // customer position
        //------------------------------------------------------------
        [Authorize(Roles = "User")]
        [Route("chgcustpos")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangeCustomerPosition([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var result = false;
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCRouteDomain();
                    result = service.SaveCustomerPosition(data.customerPointDataList.Select(x=> x.ToDMCViewModel()).ToList());

                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }

        }

        #endregion





        /*


        //-----------------------------------------------------------------------------------------------------------
        //  MAP
        //-----------------------------------------------------------------------------------------------------------
        
        //---------------------------------------
        //  load multi areas line
        //---------------------------------------
        [Authorize(Roles = "User")]
        [Route("area/LoadAreasLine")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadAreasLine([FromBody]RegionAreaRequestModel data)
        {
            try
            {
                var polies = new List<PolyView>();
                await Task.Factory.StartNew(() =>
                {
                    foreach (var areaId in data.regionAreaIds)
                    {
                        var areaPointService = new AreaPointService();
                        var areaService = new AreaService();

                        var poly = new PolyView();
                        var points = areaPointService.LoadAreaPointById(areaId);
                        var view = areaService.GetViewById(areaId);
                        poly.MasterId = view.Id;
                        poly.Lable = view.Title;
                        poly.Color = "#000000";
                        poly.Desc = "";
                        poly.Points = points;
                        poly.IsLeaf = view.IsLeaf;
                        polies.Add(poly);

                    }
                });

                return Ok(polies);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        */


    }
}
