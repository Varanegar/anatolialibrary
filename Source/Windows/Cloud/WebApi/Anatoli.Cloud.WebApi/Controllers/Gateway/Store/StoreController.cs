using Anatoli.Business.Domain;
using Anatoli.ViewModels.StoreModels;
using Anatoli.PMC.Business.Domain.Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.Concretes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/store")]
    public class StoreController : AnatoliApiController
    {

        #region Store On Hand List
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storeOnhand")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreOnhands()
        {
            try
            {
                var result = await new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storeOnhand/compress")]
        [HttpPost,GzipCompression]
        public async Task<IHttpActionResult> GetStoreOnhandsComopress()
        {
            return await GetStoreOnhands();
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storeOnhand/compress/after")]
        [HttpPost, GzipCompression]
        public async Task<IHttpActionResult> GetStoreOnhandsCompress([FromBody] StoreRequestModel data)
        {
            return await GetStoreOnhands(data);
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storeOnhand/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreOnhands([FromBody] StoreRequestModel data)
        {
            try
            {
                var validDate = GetDateFromString(data.dateAfter);
                var result = await new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storeOnhandbyid/")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreOnhandsByStoreId([FromBody] StoreRequestModel data)
        {
            try
            {
                var result = await new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllByStoreId(data.storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storeOnhandbyid/online")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreOnlineOnhandsByStoreId([FromBody] StoreRequestModel data)
        {
            try
            {
                var result = new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetAllByStoreIdOnLine(data.storeId);
                await Task.Factory.StartNew(() =>
                {
                    result.ForEach(item =>
                        {
                            item.ApplicationOwnerId = OwnerKey;
                        });
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "InternalCommunication")]
        [Route("storeOnhandbyid/local")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreLocalOnhandsByStoreId([FromBody] StoreRequestModel data)
        {
            try
            {
                var result = new List<StoreActiveOnhandViewModel>();
                await Task.Factory.StartNew(() =>
                {
                    var storeDomain = new PMCStoreOnHandDomain();
                    result = storeDomain.GetAllByStoreId(data.storeId.ToString());
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storeOnhandbyid/after/")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreOnhandsAfter([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await storeDomain.GetAllByStoreIdChangedAfter(data.storeId, validDate);
                return Ok(data.storeId);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("storeOnhand/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStoreOnhands([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeDomain.PublishAsync(new StoreActiveOnhandProxy().ReverseConvert(data.storeActiveOnhandData));
                return Ok(data.storeActiveOnhandData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("storeOnhand/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedStoreOnhands([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActiveOnhandDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeDomain.CheckDeletedAsync(data.storeActiveOnhandData);
                return Ok(data.storeActiveOnhandData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Store Price List
        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelist/compress")]
        [HttpPost]
        [GzipCompression]

        public async Task<IHttpActionResult> GetStorePriceListsCompress()
        {
            return await GetStorePriceLists();
        }

        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelist")]
        [HttpPost]

        public async Task<IHttpActionResult> GetStorePriceLists()
        {
            try
            {
                var storeDomain = new StoreActivePriceListDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await storeDomain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storepricelistbyid/")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStorePriceLists([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActivePriceListDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await storeDomain.GetAllByStoreId(data.storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storepricelist/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStorePriceListsAfter([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActivePriceListDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await storeDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storepricelist/compress/after")]
        [HttpPost, GzipCompression]
        public async Task<IHttpActionResult> GetStorePriceListsAfterCompress([FromBody] StoreRequestModel data)
        {
            return await GetStorePriceListsAfter(data);
        }


        [Authorize(Roles = "AuthorizedApp")]
        [Route("storepricelistbyid/after/")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStorePriceListsByIdAfter([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActivePriceListDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await storeDomain.GetAllByStoreIdChangedAfterAsync(data.storeId, validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("storepricelist/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SavePriceLists([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActivePriceListDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeDomain.PublishAsync(new StoreActivePriceListProxy().ReverseConvert(data.storeActivePriceListData));
                return Ok(data.storeActivePriceListData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("storepricelist/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedPriceLists([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreActivePriceListDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeDomain.CheckDeletedAsync(data.storeActivePriceListData);
                return Ok(data.storeActivePriceListData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Store List
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("stores")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStores()
        {
            try
            {
                var storeDomain = new StoreDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await storeDomain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("stores/compress")]
        [GzipCompression]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoresCompress()
        {
            return await GetStores();
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("stores/after")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStores([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var validDate = GetDateFromString(data.dateAfter);
                var result = await storeDomain.GetAllChangedAfterAsync(validDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("stores/compress/after")]
        [GzipCompression]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoresCompress([FromBody] StoreRequestModel data)
        {
            return await GetStores(data);
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStores([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeDomain.PublishAsync(new StoreProxy().ReverseConvert(data.storeData));
                return Ok(data.storeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedStores([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeDomain = new StoreDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeDomain.CheckDeletedAsync(data.storeData);
                return Ok(data.storeData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Store Calendar
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storecalendarbyid")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreCalendars([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeCalendarDomain = new StoreCalendarDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await storeCalendarDomain.GetCalendarByStoreId(data.storeId.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("storecalendar")]
        [HttpPost]
        public async Task<IHttpActionResult> GetStoreCalendars()
        {
            try
            {
                var storeCalendarDomain = new StoreCalendarDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var result = await storeCalendarDomain.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("storecalendar/save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveStoreCalendars([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeCalendarDomain = new StoreCalendarDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeCalendarDomain.PublishAsync(new StoreCalendarProxy().ReverseConvert(data.storeCalendarData));
                return Ok(data.storeCalendarData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, BaseDataAdmin")]
        [Route("storecalendar/checkdeleted")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckDeletedStoreCalendars([FromBody] StoreRequestModel data)
        {
            try
            {
                var storeCalendarDomain = new StoreCalendarDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                await storeCalendarDomain.CheckDeletedAsync(data.storeCalendarData);
                return Ok(data.storeCalendarData);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }


        #endregion
    }
}
