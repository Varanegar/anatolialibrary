using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;
using Anatoli.Rastak.DataAccess.Helpers;
using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.StoreModels;

namespace Anatoli.Rastak.DataAccess.DataAdapter
{
    public class RastakStockAdapter : RastakBaseAdapter
    {
        private static RastakStockAdapter instance = null;
        public static RastakStockAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RastakStockAdapter();
                }
                return instance;
            }
        }
        RastakStockAdapter() { }
        public List<StockViewModel> GetAllStocks(DateTime lastUpload)
        {
            try
            {
                List<StockViewModel> stockList = new List<StockViewModel>();
                using (var context = new DataContext())
                {
                    string where = "";
                    if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    var data = context.All<StockViewModel>(RastakDBQuery.Instance.GetStockQuery());
                    stockList = data.ToList();
                    stockList.ForEach(item =>
                    {
                        var ts = TimeSpan.ParseExact("0:0", @"h\:m",
                             CultureInfo.InvariantCulture);

                    });
                }

                return stockList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
        public List<StockActiveOnHandViewModel> GetAllStockOnHands(DateTime lastUpload)
        {
            string centerId = "center";
            //RastakStoreConfigEntity config = StoreConfigHeler.Instance.GetStoreConfig(1);
            //string connectionString = StoreConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;
            try
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                List<StockActiveOnHandViewModel> StockOnHandList = new List<StockActiveOnHandViewModel>();
                using (var context = new DataContext())//centerId, connectionString, Transaction.No))
                {
                    var fiscalYear = context.GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
                    var data = context.All<StockActiveOnHandViewModel>(RastakDBQuery.Instance.GetStockOnHand(fiscalYear));

                    StockOnHandList = data.ToList();
                }

                return StockOnHandList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
        public List<StockActiveOnHandViewModel> GetAllStockOnHandsByStockId(DateTime lastUpload, string stockId, string centerId)
        {
            string connectionString = RastakBranchConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;
            try
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                List<StockActiveOnHandViewModel> StockOnHandList = new List<StockActiveOnHandViewModel>();
                using (var context = new DataContext(centerId, connectionString, Transaction.No))
                {
                    var fiscalYear = context.GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
                    var data = context.All<StockActiveOnHandViewModel>(RastakDBQuery.Instance.GetStockOnHandByStockId(fiscalYear, stockId));

                    StockOnHandList = data.ToList();
                }

                return StockOnHandList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
        public List<StockProductViewModel> GetAllStockProducts(DateTime lastUpload)
        {
            string centerId = "center";
            RastakBranchConfigEntity config = RastakBranchConfigHeler.Instance.GetStoreConfig(1);
            string connectionString = RastakBranchConfigHeler.Instance.GetStoreConfig(centerId).ConnectionString;
            try
            {
                string where = "";
                if (lastUpload != DateTime.MinValue) where = " and ModifiedDate >= '" + lastUpload.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                List<StockProductViewModel> StockProductList = new List<StockProductViewModel>();
                using (var context = new DataContext())//centerId, connectionString, Transaction.No))
                {
                    var fiscalYear = context.GetValue<int>(RastakDBQuery.Instance.GetFiscalYearId());
                    var data = context.All<StockProductViewModel>(RastakDBQuery.Instance.GetStockProducts(fiscalYear) + where);

                    StockProductList = data.ToList();
                }

                return StockProductList;
            }
            catch (Exception ex)
            {
                log.Error("Failed fetch data ", ex);
                return null;
            }
        }
    }
}
