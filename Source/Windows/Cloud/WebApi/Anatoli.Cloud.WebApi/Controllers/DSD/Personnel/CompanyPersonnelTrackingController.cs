﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.Business.Domain.CompanyPersonel;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Cloud.WebApi.Handler.AutoMapper;
using Anatoli.DataAccess.Models.PersonnelAcitvity;
using Anatoli.DMC.Business.Domain;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.ViewModels.PersonnelAcitvityModel;
using Anatoli.ViewModels.RequestModel;
using Anatoli.ViewModels.VnGisModels;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.Personnel
{
    [RoutePrefix("api/dsd/tracking")]
    public class CompanyPersonnelTrackingController : AnatoliApiController
    {
        [Authorize(Roles = "User")]
        [Route("svprsact")]
        [HttpPost]
        public async Task<IHttpActionResult> SavePersonelActivitie([FromBody]PersonelTrackingRequestModel data)
        {
            try
            {
                var pointservice = new PersonnelDailyActivityPointDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                PersonnelDailyActivityPoint pointentity = null;


                var service = new PersonnelDailyActivityEventDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                PersonnelDailyActivityEvent entity = null;
                
                //order events 
                if ((data.orderEvent != null) && (data.orderEvent.Count > 0))
                {
                    foreach (var item in data.orderEvent)
                    {
                        entity = item.ToModel();
                        pointentity = item.ToPointModel();
                        entity.JData = (item.eventData).GetJson();
                        await service.SavePersonelActivitie(entity);
                        await pointservice.SavePersonelPoint(pointentity);
                    }
                }

                //lackOfOrder events
                if ((data.lackOfOrderEvent != null) && (data.lackOfOrderEvent.Count > 0))
                {
                    foreach (var item in data.lackOfOrderEvent)
                    {
                        entity = item.ToModel();
                        pointentity = item.ToPointModel();
                        entity.JData = (item.eventData).GetJson();
                        await service.SavePersonelActivitie(entity);
                        await pointservice.SavePersonelPoint(pointentity);
                    }
                }
                
                if ((data.lackOfVisitEvent != null)&& (data.lackOfVisitEvent.Count > 0))
                {
                    foreach (var item in data.lackOfVisitEvent)
                    {
                        entity = item.ToModel();
                        pointentity = item.ToPointModel();
                        entity.JData = (item.eventData).GetJson();
                        await service.SavePersonelActivitie(entity);
                        await pointservice.SavePersonelPoint(pointentity);
                    }
                }
                
                if ((data.pointEvent != null) && (data.pointEvent.Count > 0))
                {
                    foreach (var item in data.pointEvent)
                    {
                        pointentity = item.ToModel();
                        await pointservice.SavePersonelPoint(pointentity);
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }

        }

        
        
        [Authorize(Roles = "User")]
        [Route("ldprsacts")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadPersonelsActivities([FromBody]PersonelTrackingRequestModel data)
        {
            try
            {
                var service = new PersonnelDailyActivityEventDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var list = await service.LoadPersonelsEvents(data.date, data.fromTime, data.toTime, 
                        data.personelIds,
                        data.order, data.lackOrder, data.lackVisit, data.stopWithoutActivity, data.stopWithoutCustomer);

                #region ToPolyViewModel

                var points = new List<PointViewModel>();
                var color = Color.Black;

                foreach (var pActivityPoint in list)
                {

                    points.Add(new PointViewModel()
                    {
                        Id = pActivityPoint.Id,
                        MasterId = pActivityPoint.CompanyPersonnelId,
                        Latitude = pActivityPoint.Latitude,
                        Longitude = pActivityPoint.Longitude,
                        JData = pActivityPoint.JData,
                        PointType = pActivityPoint.PersonnelDailyActivityEventTypeId
                    });
                }

                #endregion

                return Ok(points);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }

        }


        [Authorize(Roles = "User")]
        [Route("ldprspth")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadPersonelsPath([FromBody]PersonelTrackingRequestModel data)
        {
            try
            {
                var service = new PersonnelDailyActivityPointDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var list =  await service.LoadPersonelsPath(data.date, data.personelIds, data.fromTime, data.toTime);

                #region ToPolyViewModel

                Guid? group = null;
                var lines = new List<PolyViewModel>();
                var line = new List<PointViewModel>();
                var color = Color.Black;

                foreach (var pActivityPoint in list)
                {
                    if (group == null)
                        group = pActivityPoint.CompanyPersonnelId;

                    if (group != pActivityPoint.CompanyPersonnelId)
                    {
                        color = GetRandomColor();

                        lines.Add(new PolyViewModel()
                        {
                            masterId = group,
                            points = line,
                            color = color.ToArgb().ToString()
                        });
                        line = new List<PointViewModel>();
                        group = pActivityPoint.CompanyPersonnelId;
                    }
                    line.Add(new PointViewModel()
                    {
                        Id = pActivityPoint.Id,
                        MasterId = pActivityPoint.CompanyPersonnelId,
                        Latitude = pActivityPoint.Latitude,
                        Longitude = pActivityPoint.Longitude,
                        Desc = "<p><br/>" + pActivityPoint.ActivityDate.ToString("HH:mm:ss") + "</p>",
                        PointType = null
                    });
                }

                color = GetRandomColor();
                lines.Add(new PolyViewModel()
                {
                    masterId = group,
                    points = line,
                    color = color.ToArgb().ToString()
                });

                #endregion

                return Ok(lines);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("ldprsprgpth")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadPersonelsProgramPath([FromBody]PersonelTrackingRequestModel data)
        {
            try
            {
                var result = new List<DMCPolyViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCRouteDomain();
                    result = service.LoadPersonelsProgramPath(data.date, data.personelIds);

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
        [Route("ldlstpnt")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadLastPoints([FromBody]PersonelTrackingRequestModel data)
        {
            try
            {
                var service = new PersonnelDailyActivityPointDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
               
                var list = await service.LoadPersonelsLastPoint(data.personelIds);

                #region ToPointViewModel

                Guid? group = null;
                var points = new List<PointViewModel>();

                foreach (var pActivityPoint in list)
                {
                    points.Add(new PointViewModel()
                    {
                        Id = pActivityPoint.Id,
                        MasterId = pActivityPoint.CompanyPersonnelId,
                        Latitude = pActivityPoint.Latitude,
                        Longitude = pActivityPoint.Longitude
                        // PointType = pActivityPoint.
                    });
                }

                #endregion

                return Ok(points);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);

                return GetErrorResult(ex);
            }
        }

        private static Color GetRandomColor()
        {
            var randonGen = new Random();
            var c = Color.FromArgb(randonGen.Next(1, 255), randonGen.Next(1, 255),
                                randonGen.Next(1, 255));
            return c;
        }

    }
}
