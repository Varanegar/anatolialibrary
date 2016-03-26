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
using Anatoli.PMC.DataAccess.Helpers.Entity;

namespace Anatoli.PMC.DataAccess.DataAdapter
{
    public class StoreAdapter : BaseAdapter
    {
        private static StoreAdapter instance = null;
        public static StoreAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreAdapter();
                }
                return instance;
            }
        }
        StoreAdapter() { }
        public List<StoreViewModel> GetAllStores(DateTime lastUpload)
        {
            try
            {
                List<StoreViewModel> storeList = new List<StoreViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StoreViewModel>(DBQuery.Instance.GetStoreQuery());
                    storeList = data.ToList();
                    storeList.ForEach(item =>
                    {
                        //var ts = TimeSpan.ParseExact("0:0", @"h\:m",
                        //     CultureInfo.InvariantCulture);

                        //var storeCalendar = context.All<StoreCalendarViewModel>(DBQuery.Instance.GetStoreCalendarQuery(item.CenterId));
                        //item.StoreCalendar = storeCalendar.ToList();

                        var storeValidRegion = context.All<CityRegionViewModel>(DBQuery.Instance.GetStoreDeliveryRegion(item.CenterId));
                        item.StoreValidRegionInfo = storeValidRegion.ToList();
                    });
                }

                return storeList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                throw ex;
            }
        }
        public List<StoreActivePriceListViewModel> GetAllStorePriceLists(DateTime lastUpload)
        {
            try
            {
                List<StoreActivePriceListViewModel> storeOnhandList = new List<StoreActivePriceListViewModel>();
                using (var context = new DataContext())
                {
                    //string where = "";
                    //if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StoreActivePriceListViewModel>(DBQuery.Instance.GetStorePriceList());

                    storeOnhandList = data.ToList();
                }

                return storeOnhandList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                throw ex;
            }
        }
        public List<StoreActiveOnhandViewModel> GetAllStoreOnHands(DateTime lastUpload)
        {
            PMCStoreConfigEntity config = StoreConfigHeler.Instance.GetStoreConfig(1);
            return GetStoreOnHands(lastUpload, "", config.ConnectionString, "center");
        }
        public List<StoreActiveOnhandViewModel> GetAllStoreOnHandsByStoreId(DateTime lastUpload, string storeId)
        {
            string connectionString = StoreConfigHeler.Instance.GetStoreConfig(storeId).ConnectionString;
            return GetStoreOnHands(lastUpload, " and Center.UniqueId='" + storeId + "'", connectionString, storeId);
        }
        private List<StoreActiveOnhandViewModel> GetStoreOnHands(DateTime lastUpload, string dynamicWhere, string connectionString, string storeId)
        {
            try
            {
                //string where = "";
                //if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                List<StoreActiveOnhandViewModel> storeOnhandList = new List<StoreActiveOnhandViewModel>();
                using (var context = new DataContext(storeId, connectionString, Transaction.No))
                {
                    var fiscalYear = context.GetValue<int>(DBQuery.Instance.GetFiscalYearId());
                    var data = context.All<StoreActiveOnhandViewModel>(DBQuery.Instance.GetStoreStockOnHand(fiscalYear) + dynamicWhere);

                    storeOnhandList = data.ToList();
                }

                return storeOnhandList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                throw ex;
            }
        }

    }
}
