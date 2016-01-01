using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.PMC.DataAccess.Helpers;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class StoreAdapter : BaseAdapter
    {
        public static List<StoreViewModel> GetAllStores(DateTime lastUpload)
        {
            try
            {
                List<StoreViewModel> storeList = new List<StoreViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StoreViewModel>(DBQuery.GetStoreQuery());
                    storeList = data.ToList();
                    storeList.ForEach(item =>
                    {
                        var ts = TimeSpan.ParseExact("0:0", @"h\:m",
                             CultureInfo.InvariantCulture);

                        var storeCalendar = context.All<StoreCalendarViewModel>(DBQuery.GetStoreCalendarQuery(item.CenterId));
                        item.StoreCalendar = storeCalendar.ToList();

                        var storeValidRegion = context.All<CityRegionViewModel>(DBQuery.GetStoreDeliveryRegion(item.CenterId));
                        item.StoreValidRegionInfo = storeValidRegion.ToList();
                    });
                }

                return storeList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
        public static List<StoreActivePriceListViewModel> GetAllStorePriceLists(DateTime lastUpload)
        {
            try
            {
                List<StoreActivePriceListViewModel> storeOnhandList = new List<StoreActivePriceListViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StoreActivePriceListViewModel>(DBQuery.GetStorePriceList() + where);

                    storeOnhandList = data.ToList();
                }

                return storeOnhandList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
        public static List<StoreActiveOnhandViewModel> GetAllStoreOnHands(DateTime lastUpload)
        {
            return GetStoreOnHands(lastUpload, "");
        }
        public static List<StoreActiveOnhandViewModel> GetAllStoreOnHandsByStoreId(DateTime lastUpload, string storeId)
        {
            return GetStoreOnHands(lastUpload, " and StoreGuid='" + storeId +"'");
        }
        private static List<StoreActiveOnhandViewModel> GetStoreOnHands(DateTime lastUpload, string dynamicWhere)
        {
            try
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                List<StoreActiveOnhandViewModel> storeOnhandList = new List<StoreActiveOnhandViewModel>();
                using (var context = new DataContext())
                {
                    var fiscalYear = context.First<int>(DBQuery.GetFiscalYearId());
                    var data = context.All<StoreActiveOnhandViewModel>(DBQuery.GetStoreStockOnHand(fiscalYear) + dynamicWhere);

                    storeOnhandList = data.ToList();
                }

                return storeOnhandList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }

    }
}
