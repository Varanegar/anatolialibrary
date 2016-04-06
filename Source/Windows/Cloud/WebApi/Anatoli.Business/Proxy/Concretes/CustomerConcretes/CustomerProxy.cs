using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.CustomerModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.Business.Proxy.CustomerConcretes
{
    public class CustomerProxy : AnatoliProxy<Customer, CustomerViewModel>, IAnatoliProxy<Customer, CustomerViewModel>
    {
        public IAnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel> CustomerShipAddressProxy { get; set; }

        #region Ctors
        public CustomerProxy() :
            this(AnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel>.Create()
            )
        { }

        public CustomerProxy(IAnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel> customerShipAddressProxy
            )
        {
            CustomerShipAddressProxy = customerShipAddressProxy;
        }
        #endregion

        public override CustomerViewModel Convert(Customer data)
        {
            return new CustomerViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ApplicationOwnerId = data.ApplicationOwnerId,
                CustomerCode = data.CustomerCode,
                CustomerName = data.CustomerName,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Phone = data.Phone,
                Mobile = data.Mobile,
                MainStreet = data.MainStreet,
                OtherStreet = data.OtherStreet,
                PostalCode = data.PostalCode,
                NationalCode = data.NationalCode,
                BirthDay = data.BirthDay,
                Email = data.Email,
                RegionInfoId = data.RegionInfoId,
                RegionLevel1Id = data.RegionLevel1Id,
                RegionLevel2Id = data.RegionLevel2Id,
                RegionLevel3Id = data.RegionLevel3Id,
                RegionLevel4Id = data.RegionLevel4Id,
                DefauleStoreId = data.DefauleStoreId,
                CompanyId = data.CompanyId,
                IsRemoved = data.IsRemoved,

                CustomerShipAddress  = (data.CustomerShipAddresses != null)?CustomerShipAddressProxy.Convert(data.CustomerShipAddresses.ToList()):null,
            };
        }

        public override Customer ReverseConvert(CustomerViewModel data)
        {
            return new Customer
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,
                IsRemoved = data.IsRemoved,

                CustomerCode = data.CustomerCode,
                CustomerName = data.CustomerName,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Phone = data.Phone,
                Mobile = data.Mobile,
                MainStreet = data.MainStreet,
                OtherStreet = data.OtherStreet,
                PostalCode = data.PostalCode,
                NationalCode = data.NationalCode,
                BirthDay = data.BirthDay,
                Email = data.Email,
                RegionInfoId = data.RegionInfoId,
                RegionLevel1Id = data.RegionLevel1Id,
                RegionLevel2Id = data.RegionLevel2Id,
                RegionLevel3Id = data.RegionLevel3Id,
                RegionLevel4Id = data.RegionLevel4Id,
                DefauleStoreId = data.DefauleStoreId,
                CompanyId = data.CompanyId,

                CustomerShipAddresses = (data.CustomerShipAddress != null) ? CustomerShipAddressProxy.ReverseConvert(data.CustomerShipAddress.ToList()) : null,
            };
        }
    }
}