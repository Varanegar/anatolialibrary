using Anatoli.PMC.DataAccess.Helpers.Entity;
using Anatoli.PMC.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.Helpers
{
    public class StoreConfigHeler
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static PMCStoreConfigEntity currentConfig = null;
        private static List<PMCStoreConfigEntity> allStoreConfigs = new List<PMCStoreConfigEntity>();
        private static StoreConfigHeler instance = null;
        public static StoreConfigHeler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StoreConfigHeler();
                }
                return instance;
            }
        }
        StoreConfigHeler()
        {
            using (DataContext context = new DataContext())
            {
                DataObject<PMCStoreConfigEntity> configDataObject = new DataObject<PMCStoreConfigEntity>("Center");
                allStoreConfigs= configDataObject.Select.All().ToList();
            }
        }

        public PMCStoreConfigEntity CurrentConfig
        {
            get
            {
                using (DataContext context = new DataContext())
                {
                    if (currentConfig == null)
                    {
                        DataObject<PMCStoreConfigEntity> configDataObject = new DataObject<PMCStoreConfigEntity>("Center");
                        currentConfig = configDataObject.Select.First("where centerId in (select top 1 centerid from CenterSetting)");

                    }
                    currentConfig.FiscalYearId = context.First<int>(DBQuery.GetFiscalYearId());
                }

                return currentConfig;
            }
        }

        public PMCStoreConfigEntity GetStoreConfig(string storeUniqueId)
        {
            var config = allStoreConfigs.Find(p => p.UniqueId == storeUniqueId);
            config.FiscalYearId = new DataContext().First<int>(DBQuery.GetFiscalYearId());
            return config;
        }

        public PMCStoreConfigEntity GetStoreConfig(int storeId)
        {
            var config = allStoreConfigs.Find(p => p.CenterId == storeId);
            config.FiscalYearId = new DataContext().First<int>(DBQuery.GetFiscalYearId());
            return config;
        }
    }
}
