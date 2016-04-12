using System;
using System.Linq;
using Anatoli.Business.Proxy;
using System.Threading.Tasks;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.DataAccess.Repositories;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.CustomerModels;

namespace Anatoli.Business.Domain
{
    public class CustomerShipAddressDomain : BusinessDomainV2<CustomerShipAddress, CustomerShipAddressViewModel, CustomerShipAddressRepository, ICustomerShipAddressRepository>, IBusinessDomainV2<CustomerShipAddress, CustomerShipAddressViewModel>
    {
        #region Properties
        #endregion

        #region Ctors
        public CustomerShipAddressDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey)
            : this(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, new AnatoliDbContext())
        {

        }
        public CustomerShipAddressDomain(Guid applicationOwnerKey, Guid dataOwnerKey, Guid dataOwnerCenterKey, AnatoliDbContext dbc)
            : base(applicationOwnerKey, dataOwnerKey, dataOwnerCenterKey, dbc)
        {
        }        

        #endregion

        #region Methods
        public async Task<ICollection<CustomerShipAddressViewModel>> GetCustomerShipAddressById(Guid customerId, bool? isDetault, bool? isActive)
        {
            ICollection<CustomerShipAddressViewModel> customerShipAddresses = null;
            if (isDetault == null && isActive == null)
                customerShipAddresses = await GetAllAsync(p => p.CustomerId == customerId);
            else if (isDetault == true)
                customerShipAddresses = await GetAllAsync(p => p.CustomerId == customerId && p.IsDefault);
            else if (isActive == true)
                customerShipAddresses = await GetAllAsync(p => p.CustomerId == customerId && p.IsActive);

            return customerShipAddresses;
        }
        public async Task<ICollection<CustomerShipAddressViewModel>> GetCustomerShipAddressByLevel4(Guid customerId, Guid levelId)
        {
            return await GetAllAsync(p => p.DataOwnerId == DataOwnerKey && p.CustomerId == customerId && p.RegionLevel4Id == levelId);
        }
        protected override void AddDataToRepository(CustomerShipAddress currentCustomerShipAddress, CustomerShipAddress item)
        {
            if (item.Id == null || item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();

            if (currentCustomerShipAddress != null)
            {
                currentCustomerShipAddress.Phone = item.Phone;
                currentCustomerShipAddress.AddressName = item.AddressName;
                currentCustomerShipAddress.Email = item.Email;
                currentCustomerShipAddress.MainStreet = item.MainStreet;
                currentCustomerShipAddress.OtherStreet = item.OtherStreet;
                currentCustomerShipAddress.Mobile = item.Mobile;
                currentCustomerShipAddress.PostalCode = item.PostalCode;
                currentCustomerShipAddress.LastUpdate = DateTime.Now;
                currentCustomerShipAddress.DefauleStore_Id = item.DefauleStore_Id;
                currentCustomerShipAddress.RegionInfoId = item.RegionInfoId;
                currentCustomerShipAddress.RegionLevel1Id = item.RegionLevel1Id;
                currentCustomerShipAddress.RegionLevel2Id = item.RegionLevel2Id;
                currentCustomerShipAddress.RegionLevel3Id = item.RegionLevel3Id;
                currentCustomerShipAddress.RegionLevel4Id = item.RegionLevel4Id;
                currentCustomerShipAddress.MainStreet = item.MainStreet;
                currentCustomerShipAddress.OtherStreet = item.OtherStreet;
                currentCustomerShipAddress.Lat = item.Lat;
                currentCustomerShipAddress.Lng = item.Lng;
                currentCustomerShipAddress.CustomerId = item.CustomerId;
                currentCustomerShipAddress.IsActive = item.IsActive;
                currentCustomerShipAddress.IsDefault = item.IsDefault;
                currentCustomerShipAddress.Transferee = item.Transferee;
                MainRepository.Update(currentCustomerShipAddress);
            }
            else
            {
                item.CreatedDate = item.LastUpdate = DateTime.Now;
                MainRepository.Add(item);
            }
        }
        #endregion
    }
}
