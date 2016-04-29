using Anatoli.DMC.DataAccess.Helpers.Entity;
using Anatoli.DMC.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.DMC.DataAccess.Helpers
{
    public class BranchConfigHeler
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static DMCBranchConfigEntity currentConfig = null;
        public List<DMCBranchConfigEntity> AllStoreConfigs { get; private set; }

        private static BranchConfigHeler instance = null;
        public static BranchConfigHeler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BranchConfigHeler();
                }
                return instance;
            }
        }
        BranchConfigHeler()
        {
            using (DataContext context = new DataContext())
            {
                DataObject<DMCBranchConfigEntity> configDataObject = new DataObject<DMCBranchConfigEntity>("Center");
                AllStoreConfigs= configDataObject.Select.All("where isReal=1").ToList();
            }
        }

        public DMCBranchConfigEntity CurrentConfig
        {
            get
            {
                using (DataContext context = new DataContext())
                {
                    if (currentConfig == null)
                    {
                        DataObject<DMCBranchConfigEntity> configDataObject = new DataObject<DMCBranchConfigEntity>("Center");
                        currentConfig = configDataObject.Select.First("where centerId in (select top 1 centerid from CenterSetting)");

                    }
                    currentConfig.FiscalYearId = context.GetValue<int>(DBQuery.Instance.GetFiscalYearId());
                }

                return currentConfig;
            }
        }

        public DMCBranchConfigEntity GetStoreConfig(string storeUniqueId)
        {
            var config = AllStoreConfigs.Find(p => p.UniqueId.ToLower() == storeUniqueId.ToLower());
            config.FiscalYearId = new DataContext().GetValue<int>(DBQuery.Instance.GetFiscalYearId());
            return config;
        }

        public DMCBranchConfigEntity GetStoreConfig(int storeId)
        {
            var config = AllStoreConfigs.Find(p => p.CenterId == storeId);
            config.FiscalYearId = new DataContext().GetValue<int>(DBQuery.Instance.GetFiscalYearId());
            return config;
        }
    }
}
