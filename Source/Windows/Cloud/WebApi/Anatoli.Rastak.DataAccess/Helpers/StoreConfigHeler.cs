using Anatoli.Rastak.DataAccess.Helpers.Entity;
using Anatoli.Rastak.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.Rastak.DataAccess.Helpers
{
    public class StoreConfigHeler
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static RastakStoreConfigEntity currentConfig = null;
        public List<RastakStoreConfigEntity> AllStoreConfigs { get; private set; }

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
                DataObject<RastakStoreConfigEntity> configDataObject = new DataObject<RastakStoreConfigEntity>("Center");
                AllStoreConfigs= configDataObject.Select.All("where isReal=1").ToList();
            }
        }

        public RastakStoreConfigEntity CurrentConfig
        {
            get
            {
                using (DataContext context = new DataContext())
                {
                    if (currentConfig == null)
                    {
                        DataObject<RastakStoreConfigEntity> configDataObject = new DataObject<RastakStoreConfigEntity>("Center");
                        currentConfig = configDataObject.Select.First("where centerId in (select top 1 centerid from CenterSetting)");

                    }
                    currentConfig.FiscalYearId = context.GetValue<int>(DBQuery.Instance.GetFiscalYearId());
                }

                return currentConfig;
            }
        }

        public RastakStoreConfigEntity GetStoreConfig(string storeUniqueId)
        {
            var config = AllStoreConfigs.Find(p => p.UniqueId.ToLower() == storeUniqueId.ToLower());
            config.FiscalYearId = new DataContext().GetValue<int>(DBQuery.Instance.GetFiscalYearId());
            return config;
        }

        public RastakStoreConfigEntity GetStoreConfig(int storeId)
        {
            var config = AllStoreConfigs.Find(p => p.CenterId == storeId);
            config.FiscalYearId = new DataContext().GetValue<int>(DBQuery.Instance.GetFiscalYearId());
            return config;
        }
    }
}
