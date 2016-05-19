using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.StoreModels;
using Anatoli.ViewModels.BaseModels;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Proxy.Concretes
{
    public class StoreProxy : AnatoliProxy<Store, StoreViewModel>, IAnatoliProxy<Store, StoreViewModel>
    {
        public IAnatoliProxy<StoreCalendar, StoreCalendarViewModel> StoreCalendarProxy { get; set; }
        public IAnatoliProxy<CityRegion, CityRegionViewModel> CityRegionProxy { get; set; }

        #region Ctors
        public StoreProxy() :
            this(AnatoliProxy<CityRegion, CityRegionViewModel>.Create(), AnatoliProxy<StoreCalendar, StoreCalendarViewModel>.Create()
            )
        { }

        public StoreProxy(IAnatoliProxy<CityRegion, CityRegionViewModel> cityRegionProxy,
            IAnatoliProxy<StoreCalendar, StoreCalendarViewModel> storeCalendarProxy
            )
        {
            StoreCalendarProxy = storeCalendarProxy;
            CityRegionProxy = cityRegionProxy;
        }
        #endregion

        public override StoreViewModel Convert(Store data)
        {
            return new StoreViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                IsRemoved = data.IsRemoved,
                ApplicationOwnerId = data.ApplicationOwnerId,
                StoreCode = data.StoreCode,
                StoreName = data.StoreName,
                Address = data.Address,
                Phone = data.Phone,
                HasDelivery = data.HasDelivery,
                HasCourier = data.HasCourier,
                SupportAppOrder = data.SupportAppOrder,
                SupportCallCenterOrder = data.SupportCallCenterOrder,
                SupportWebOrder = data.SupportWebOrder,
                Lat = data.Lat,
                Lng = data.Lng,
                StoreCalendar = StoreCalendarProxy.Convert(data.StoreCalendars.ToList()),
                StoreValidRegionInfo = CityRegionProxy.Convert(data.StoreValidRegionInfoes.ToList()),

            };
        }

        public override Store ReverseConvert(StoreViewModel data)
        {
            Store temp = new Store()
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                ApplicationOwnerId = data.ApplicationOwnerId,
                IsRemoved = data.IsRemoved,

                StoreCode = data.StoreCode,
                StoreName = data.StoreName,
                Address = data.Address,
                Phone = data.Phone,
                HasDelivery = data.HasDelivery,
                HasCourier = data.HasCourier,
                SupportAppOrder = data.SupportAppOrder,
                SupportCallCenterOrder = data.SupportCallCenterOrder,
                SupportWebOrder = data.SupportWebOrder,
                Lat = data.Lat,
                Lng = data.Lng,
                StoreCalendars = StoreCalendarProxy.ReverseConvert(data.StoreCalendar.ToList()),
            };
            temp.StoreValidRegionInfoes = CityRegionProxy.ReverseConvert(data.StoreValidRegionInfo.ToList());

            return temp;
        }
    }
}
