using Anatoli.Business.Domain;
using Anatoli.Cloud.WebApi.Classes;
using Anatoli.Cloud.WebApi.Infrastructure;
using Anatoli.DataAccess;
using Anatoli.ViewModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using Anatoli.DataAccess.Repositories;
using Anatoli.ViewModels;
using Anatoli.Business.Proxy.CustomerConcretes;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/customer")]
    public class CustomerController : AnatoliApiController
    {
        #region Customer
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customers")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerById([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new CustomerDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetByIdAsync(data.customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("savesingle")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveSingleCustomer([FromBody]CustomerRequestModel data)
        {
            try
            {
                var customerDomain = new CustomerDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                var userDomain = new UserDomain(OwnerKey);
                if (data.customerData.Email != null)
                {
                    var emailUser = await userDomain.GetByEmailAsync(data.customerData.Email);
                    if (emailUser != null && data.customerData.UniqueId.ToString() != emailUser.Id)
                        return GetErrorResult("ايميل شما قبلا استفاده شده است");
                }

                if (data.customerData.Phone != null)
                {
                    var pheonUser = await userDomain.GetByEmailAsync(data.customerData.Phone);
                    if (pheonUser != null && data.customerData.UniqueId.ToString() != pheonUser.Id)
                        return GetErrorResult("موبايل شما قبلا استفاده شده است");
                }
                var user = await userDomain.GetByIdAsync(data.customerData.UniqueId);
                if (user != null)
                {
                    var userStore = new AnatoliUserStore(Request.GetOwinContext().Get<AnatoliDbContext>());
                    if (user.Email != data.customerData.Email)
                        await userStore.ChangeEmailAddress(user, data.customerData.Email);
                }



                var saveData = new CustomerProxy().ReverseConvert(data.customerData);
                await customerDomain.PublishAsync(saveData);

                return Ok(data.customerData);

            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        
        [Authorize(Roles = "DataSync")]
        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveBatchCustomer([FromBody]CustomerRequestModel data)
        {
            try
            {
                var customerDomain = new CustomerDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey);

                var saveData = new CustomerProxy().ReverseConvert(data.customerListData);
                await customerDomain.PublishAsync(saveData);

                return Ok(data.customerListData);

            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Customer Ship Address
        [Authorize(Roles = "User")]
        [Route("customershipaddresses/active")]
        [HttpPost]
        public async Task<IHttpActionResult> GetActiveCustomerShipAddressByCustomerId([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new CustomerShipAddressDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetCustomerShipAddressById(data.customerId, null, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("customershipaddresses/default")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDefaultCustomerShipAddressByCustomerId([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new CustomerShipAddressDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetCustomerShipAddressById(data.customerId, true, null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "User")]
        [Route("customershipaddresses")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerShipAddressByCustomerId([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new CustomerShipAddressDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetCustomerShipAddressById(data.customerId, null, null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customershipaddresses/region")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerShipAddressByCustomerIdByLevel4([FromBody]CustomerRequestModel data)
        {
            try
            {
                var result = await new CustomerShipAddressDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).GetCustomerShipAddressByLevel4(data.customerId, data.regionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "DataSync, User")]
        [Route("customershipaddress/savesingle")]
        [HttpPost]
        async Task<IHttpActionResult> SaveSingleCustomerShipAddress([FromBody]CustomerRequestModel data)
        {
            try
            {
                List<CustomerShipAddressViewModel> dataList = new List<CustomerShipAddressViewModel>();
                dataList.Add(data.customerShipAddressData);
                var saveData = new CustomerShipAddressProxy().ReverseConvert(dataList);
                await new CustomerShipAddressDomain(OwnerKey, DataOwnerKey, DataOwnerCenterKey).PublishAsync(saveData);
                return Ok(dataList);
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
