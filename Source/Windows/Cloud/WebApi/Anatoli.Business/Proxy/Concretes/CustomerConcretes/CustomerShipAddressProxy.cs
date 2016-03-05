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
    public class CustomerShipAddressProxy : AnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel>, IAnatoliProxy<CustomerShipAddress, CustomerShipAddressViewModel>
    {

        public override CustomerShipAddressViewModel Convert(CustomerShipAddress data)
        {
            return new CustomerShipAddressViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                PrivateOwnerId = data.PrivateLabelOwner.Id,
                AddressName = data.AddressName,
                Phone = data.Phone,
                Mobile = data.Mobile,
                MainStreet = data.MainStreet,
                OtherStreet = data.OtherStreet,
                PostalCode = data.PostalCode,
                Email = data.Email,
                RegionInfoId = data.RegionInfoId,
                RegionLevel1Id = data.RegionLevel1Id,
                RegionLevel2Id = data.RegionLevel2Id,
                RegionLevel3Id = data.RegionLevel3Id,
                RegionLevel4Id = data.RegionLevel4Id,
                DefauleStoreId = data.DefauleStore_Id,
                Transferee = data.Transferee,
                Lat = data.Lat,
                Lng = data.Lng,
                CustomerId = data.CustomerId,
                IsActive = data.IsActive,
                IsDefault = data.IsDefault,
                IsRemoved = data.IsRemoved,

            };
        }

        public override CustomerShipAddress ReverseConvert(CustomerShipAddressViewModel data)
        {
            return new CustomerShipAddress
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
                AddressName = data.AddressName,
                Phone = data.Phone,
                Mobile = data.Mobile,
                MainStreet = data.MainStreet,
                OtherStreet = data.OtherStreet,
                PostalCode = data.PostalCode,
                Email = data.Email,
                RegionInfoId = data.RegionInfoId,
                RegionLevel1Id = data.RegionLevel1Id,
                RegionLevel2Id = data.RegionLevel2Id,
                RegionLevel3Id = data.RegionLevel3Id,
                RegionLevel4Id = data.RegionLevel4Id,
                DefauleStore_Id = data.DefauleStoreId,
                Transferee = data.Transferee,
                Lat = data.Lat,
                Lng = data.Lng,
                CustomerId = data.CustomerId,
                IsActive = data.IsActive,
                IsDefault = data.IsDefault,
                IsRemoved = data.IsRemoved,

            };
        }
    }
}