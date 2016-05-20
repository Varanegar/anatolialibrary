using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.Business.Domain.CompanyPersonel;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Cloud.WebApi.Handler.AutoMapper;
using Anatoli.DMC.Business.Domain;
using Anatoli.DMC.ViewModels.Gis;
using Anatoli.ViewModels.CommonModels;
using Anatoli.ViewModels.RequestModel;
using Anatoli.ViewModels.VnGisModels;
using System.Drawing;

namespace Anatoli.Cloud.WebApi.Controllers.DSD.Personnel
{
    [RoutePrefix("api/dsd/personnel")]
    public class CompanyPersonnelController : AnatoliApiController
    {

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(true);
        }

        [Authorize(Roles = "User")]
        [Route("ldgrpbyarea")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadGroupByArea([FromBody]PersonelRequestModel data)
        {
            try
            {
                var result = new List<SelectListItemViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCCompanyPersonelDomain();
                    result = service.LoadGroupByArea(data.regionAreaId);

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
        [Route("ldperbygrp")]
        [HttpPost]
        public async Task<IHttpActionResult> LoadPersonByGroup([FromBody]PersonelRequestModel data)
        {
            try
            {
                var result = new List<SelectListItemViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var service = new DMCCompanyPersonelDomain();
                    result = service.LoadPersonByGroup(data.groupId);

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
