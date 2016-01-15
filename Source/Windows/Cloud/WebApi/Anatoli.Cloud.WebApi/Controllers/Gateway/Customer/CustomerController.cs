using Anatoli.Business.Domain;
using Anatoli.ViewModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Anatoli.Cloud.WebApi.Controllers
{
    [RoutePrefix("api/gateway/customer")]
    public class BasketController : BaseApiController
    {
        #region Customer
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customers")]
        public async Task<IHttpActionResult> GetCustomerById(string privateOwnerId, string id)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerDomain(owner);
                var result = await customerDomain.GetCustomerById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveCustomer(string privateOwnerId, CustomerViewModel data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerDomain(owner);
                List<CustomerViewModel> dataList = new List<CustomerViewModel>();
                dataList.Add(data);
                var result = await customerDomain.PublishAsync(dataList);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }
        #endregion

        #region Customer Ship Address
        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customershipaddresses/active")]
        public async Task<IHttpActionResult> GetActiveCustomerShipAddressByCustomerId(string privateOwnerId, string customerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerShipAddressDomain(owner);
                var result = await customerDomain.GetCustomerShipAddressById(customerId, null, true);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customershipaddresses/default")]
        public async Task<IHttpActionResult> GetDefaultCustomerShipAddressByCustomerId(string privateOwnerId, string customerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerShipAddressDomain(owner);
                var result = await customerDomain.GetCustomerShipAddressById(customerId, true, null);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("customershipaddresses")]
        public async Task<IHttpActionResult> GetCustomerShipAddressByCustomerId(string privateOwnerId, string customerId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerShipAddressDomain(owner);
                var result = await customerDomain.GetCustomerShipAddressById(customerId, null, null);

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
        public async Task<IHttpActionResult> GetCustomerShipAddressByCustomerIdByLevel4(string privateOwnerId, string customerId, string regionId)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerShipAddressDomain(owner);
                var result = await customerDomain.GetCustomerShipAddressById(customerId, null, null);

                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Web API Call Error", ex);
                return GetErrorResult(ex);
            }
        }

        [Authorize(Roles = "AuthorizedApp, User")]
        [Route("save")]
        public async Task<IHttpActionResult> SaveCustomer(string privateOwnerId, CustomerShipAddressViewModel data)
        {
            try
            {
                var owner = Guid.Parse(privateOwnerId);
                var customerDomain = new CustomerShipAddressDomain(owner);
                List<CustomerShipAddressViewModel> dataList = new List<CustomerShipAddressViewModel>();
                dataList.Add(data);
                var result = await customerDomain.PublishAsync(dataList);
                return Ok(result);
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
