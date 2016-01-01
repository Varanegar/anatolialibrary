using Anatoli.Business;
using Anatoli.Business.Domain;
using Anatoli.ViewModels.StoreModels;
using Anatoli.PMC.Business.Domain.Store;
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

        #region Store On Hand List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhand")]
        public async Task<IHttpActionResult> GetStoreOnhands(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActiveOnhandDomain(owner);
            var result = await storeDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhand/after")]
        public async Task<IHttpActionResult> GetStoreOnhands(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActiveOnhandDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await storeDomain.GetAllChangedAfter(validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhandbyid/")]
        public async Task<IHttpActionResult> GetStoreOnhandsByStoreId(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActiveOnhandDomain(owner);
            var result = await storeDomain.GetAllByStoreId(id);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhandbyid/online")]
        public async Task<IHttpActionResult> GetStoreOnlineOnhandsByStoreId(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActiveOnhandDomain(owner);
            var result = await storeDomain.GetAllByStoreIdOnLine(id);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhandbyid/local")]
        public async Task<IHttpActionResult> GetStoreOnlineOnhandsByStoreId(string id)
        {
            var result = new List<StoreActiveOnhandViewModel>();
            await Task.Factory.StartNew(() =>
            {
                var storeDomain = new PMCStoreOnHandDomain();
                result = storeDomain.GetAllByStoreId(id);
            });
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhandbyid/after/")]
        public async Task<IHttpActionResult> GetStoreOnhandsAfter(string privateOwnerId, string id, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActiveOnhandDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await storeDomain.GetAllByStoreIdChangedAfter(id, validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhand/save")]
        public async Task<IHttpActionResult> SaveStoreOnhands(string privateOwnerId, List<StoreActiveOnhandViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActiveOnhandDomain(owner);
            await storeDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Store Price List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelist")]
        public async Task<IHttpActionResult> GetStorePriceLists(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActivePriceListDomain(owner);
            var result = await storeDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelistbyid/")]
        public async Task<IHttpActionResult> GetStorePriceLists(string privateOwnerId, string id)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActivePriceListDomain(owner);
            var result = await storeDomain.GetAllByStoreId(id);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelist/after")]
        public async Task<IHttpActionResult> GetStorePriceListsAfter(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActivePriceListDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await storeDomain.GetAllChangedAfter(validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelistbyid/after/")]
        public async Task<IHttpActionResult> GetStorePriceListsAfter(string privateOwnerId, string id, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActivePriceListDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await storeDomain.GetAllByStoreIdChangedAfter(id, validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelist/save")]
        public async Task<IHttpActionResult> SavePriceLists(string privateOwnerId, List<StoreActivePriceListViewModel> data)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreActivePriceListDomain(owner);
            await storeDomain.PublishAsync(data);
            return Ok();
        }
        #endregion

        #region Store List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("stores")]
        public async Task<IHttpActionResult> GetStores(string privateOwnerId)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreDomain(owner);
            var result = await storeDomain.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("stores/after")]
        public async Task<IHttpActionResult> GetStores(string privateOwnerId, string dateAfter)
        {
            var owner = Guid.Parse(privateOwnerId);
            var storeDomain = new StoreDomain(owner);
            var validDate = DateTime.Parse(dateAfter);
            var result = await storeDomain.GetAllChangedAfter(validDate);
            return Ok(result);
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveStores(string privateOwnerId, List<StoreViewModel> data)
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
        public async Task<IHttpActionResult> GetStoreCalendars(string privateOwnerId, string id)
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
