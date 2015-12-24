using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace ClientApp
{
    public static class StoreManagement
    {
        public static List<StoreViewModel> GetStoreInfo()
        {
            List<StoreViewModel> storeList = new List<StoreViewModel>();
            using (var context = new DataContext())
            {
                var data = context.All<StoreViewModel>("SELECT CenterCode as StoreCode, CenterName as StoreName, Address, 0 as Lat, 0 as Lng, 0 as Hasdelivery, 1 as HasCourier, 1 as SupportAppOrder, 1 as SupportWebOrder, 0 as SupportCallCenterOrder FROM Center where centertypeid=3");
                storeList = data.ToList();
                storeList.ForEach(item =>
                    {
                        var storeCalendar = context.All<StoreCalendarViewModel>("select WorkingDate as PDate, dbo.select dbo.ToMiladi(WorkingDate), FormHour as FromTime, ToHour as ToTime from DayTimeWorking where CenterId = " + item.CenterId);
                        item.StoreCalendar = storeCalendar.ToList();

                        var storeValidRegion = context.All<CityRegionViewModel>("select WorkingDate as PDate, dbo.select dbo.ToMiladi(WorkingDate), FormHour as FromTime, ToHour as ToTime from DayTimeWorking where CenterId = " + item.CenterId);
                        item.StoreCalendar = storeCalendar.ToList();
                    });

            }

            return storeList;
        }
    }
}
