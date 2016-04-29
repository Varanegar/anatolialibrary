﻿using Anatoli.ViewModels.BaseModels;
using Anatoli.ViewModels.StoreModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.DataAccess.Helpers.Entity;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class RastakStoreAdapter : RastakBaseAdapter
    {
        private static RastakStoreAdapter instance = null;
        public static RastakStoreAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakStoreAdapter();
                }
                return instance;
            }
        }
        RastakStoreAdapter() { }
        public List<StoreViewModel> GetAllStores(DateTime lastUpload)
        {
            try
            {
                List<StoreViewModel> storeList = new List<StoreViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StoreViewModel>(RastakDBQuery.Instance.GetStoreQuery());
                    storeList = data.ToList();
                    storeList.ForEach(item =>
                    {
                        var ts = TimeSpan.ParseExact("0:0", @"h\:m",
                             CultureInfo.InvariantCulture);

                        var storeCalendar = context.All<StoreCalendarViewModel>(RastakDBQuery.Instance.GetStoreCalendarQuery(item.CenterId));
                        item.StoreCalendar = storeCalendar.ToList();

                        var storeValidRegion = context.All<CityRegionViewModel>(RastakDBQuery.Instance.GetStoreDeliveryRegion(item.CenterId));
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
        public List<StoreActivePriceListViewModel> GetAllStorePriceLists(DateTime lastUpload)
        {
            try
            {
                List<StoreActivePriceListViewModel> storeOnhandList = new List<StoreActivePriceListViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StoreActivePriceListViewModel>(RastakDBQuery.Instance.GetStorePriceList() + where);

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
        public List<StoreActiveOnhandViewModel> GetAllStoreOnHands(DateTime lastUpload)
        {
            RastakBranchConfigEntity config = RastakBranchConfigHeler.Instance.GetStoreConfig(1);
            return GetStoreOnHands(lastUpload, "", config.ConnectionString, "center");
        }
        public List<StoreActiveOnhandViewModel> GetAllStoreOnHandsByStoreId(DateTime lastUpload, string storeId)
        {
            string connectionString = RastakBranchConfigHeler.Instance.GetStoreConfig(storeId).ConnectionString;
            return GetStoreOnHands(lastUpload, " and Center.UniqueId='" + storeId + "'", connectionString, storeId);
        }
        private List<StoreActiveOnhandViewModel> GetStoreOnHands(DateTime lastUpload, string dynamicWhere, string connectionString, string storeId)
        {
            try
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                List<StoreActiveOnhandViewModel> storeOnhandList = new List<StoreActiveOnhandViewModel>();
                using (var context = new DataContext(storeId, connectionString, Transaction.No))
                {
                    var fiscalYear = context.GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
                    var data = context.All<StoreActiveOnhandViewModel>(RastakDBQuery.Instance.GetStoreStockOnHand(fiscalYear) + dynamicWhere);

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
