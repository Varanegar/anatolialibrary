using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Business.Proxy.Concretes
{
    public class StoreProxy : AnatoliProxy<Store, StoreViewModel>, IAnatoliProxy<Store, StoreViewModel>
    {

        public override StoreViewModel Convert(Store data)
        {
            return new StoreViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,

                PrivateLabelKey = data.PrivateLabelOwner.Id,
                StoreCode = data.StoreCode,
                StoreName = data.StoreName,
                Address = data.Address,
                HasDelivery = (data.HasDelivery == 0) ? false : true,
                HasCourier = (data.HasCourier == 0) ? false : true,
                SupportAppOrder = (data.SupportAppOrder == 0) ? false : true,
                SupportCallCenterOrder = (data.SupportCallCenterOrder == 0) ? false : true,
                SupportWebOrder = (data.SupportWebOrder == 0) ? false : true,
                Lat = data.Lat,
                Lng = data.Lng,

            };
        }

        public override Store ReverseConvert(StoreViewModel data)
        {
            return new Store()
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                StoreCode = data.StoreCode,
                StoreName = data.StoreName,
                Address = data.Address,
                HasDelivery = (byte)(data.HasDelivery ? 1 : 0),
                HasCourier = (byte)(data.HasCourier ? 1 : 0),
                SupportAppOrder = (byte)(data.SupportAppOrder ? 1 : 0),
                SupportCallCenterOrder = (byte)(data.SupportCallCenterOrder ? 1 : 0),
                SupportWebOrder = (byte)(data.SupportWebOrder ? 1 : 0),
                Lat = data.Lat,
                Lng = data.Lng,
                

            };
        }
    }
}
