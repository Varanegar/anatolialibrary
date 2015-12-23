using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.Cloud.Gateway.Business.Store;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/store")]
    public class StoreController : ApiController
    {
        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeonhand")]
        public IHttpActionResult GetStoreOnHand()
        {
            return Ok(StoreOnHandCloudHandler.GetInstance().GetSampleData());
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelist")]
        public IHttpActionResult GetStorePriceLists()
        {
            return Ok(StoreProductPriceListsCloudHandler.GetInstance().GetSampleData());
        }

        #region Store List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stores")]
        public async Task<IHttpActionResult> GetStoreLists(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreDomain(owner);
            var result = await storeDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stores/save")]
        public async Task<IHttpActionResult> SaveProductGroups(string privateOwnerId, List<StoreViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreDomain(owner);
            await storeDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Store Calendar
        [Authorize(Roles = "AuthorizedApp")]
        [Route("storecalendar")]
        public async Task<IHttpActionResult> GetStoreCalendarLists(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeCalendarDomain = new StoreCalendarDomain(owner);
            var result = await storeCalendarDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storecalendar/save")]
        public async Task<IHttpActionResult> SaveStoreCalendars(string privateOwnerId, List<StoreCalendarViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeCalendarDomain = new StoreCalendarDomain(owner);
            await storeCalendarDomain.PublishAsync(data);
            return Ok();
        }
        #endregion
    }
}
