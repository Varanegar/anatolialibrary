﻿using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.ViewModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/productrate")]
    public class ProductRateController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region ProductRates
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productrates")]
        public async Task<IHttpActionResult> GetProductRates(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productRateDomain = new ProductRateDomain(owner);
            var result = await productRateDomain.GetAll();

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productrateavgs")]
        public async Task<IHttpActionResult> GetProductRateAvgs(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productRateDomain = new ProductRateDomain(owner);
            var result = await productRateDomain.GetAllAvg();

            return Ok(result);
        }
        
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("productrateavgs/after")]
        public async Task<IHttpActionResult> GetProductRates(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var productRateDomain = new ProductRateDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await productRateDomain.GetAllAvgChangeAfter(validDate);

            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveProductRates(string privateOwnerId, List<ProductRateViewModel> data)
        {
            if (data != null) log.Info("save product count : " + data.Count);
            var owner = Guid.Parse(privateOwnerId);
            var productRateDomain = new ProductRateDomain(owner);
            var result = await productRateDomain.PublishAsyncWithReturn(data);
            return Ok(result);
        }        
        #endregion
    }
}
