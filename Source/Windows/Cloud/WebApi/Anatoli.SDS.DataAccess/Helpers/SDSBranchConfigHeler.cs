using Anatoli.SDS.DataAccess.Helpers.Entity;
using Anatoli.SDS.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.SDS.DataAccess.Helpers
{
    public class SDSBranchConfigHeler
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

        private static SDSBranchConfigEntity currentConfig = null;
        public List<SDSBranchConfigEntity> AllStoreConfigs { get; private set; }

        private static SDSBranchConfigHeler instance = null;
        public static SDSBranchConfigHeler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SDSBranchConfigHeler();
                }
                return instance;
            }
        }
        SDSBranchConfigHeler()
        {
            using (DataContext context = new DataContext())
            {
                DataObject<SDSBranchConfigEntity> configDataObject = new DataObject<SDSBranchConfigEntity>("Center");
                AllStoreConfigs= configDataObject.Select.All("where isReal=1").ToList();
            }
        }

        public SDSBranchConfigEntity CurrentConfig
        {
            get
            {
                using (DataContext context = new DataContext())
                {
                    if (currentConfig == null)
                    {
                        DataObject<SDSBranchConfigEntity> configDataObject = new DataObject<SDSBranchConfigEntity>("Center");
                        currentConfig = configDataObject.Select.First("where centerId in (select top 1 centerid from CenterSetting)");

                    }
                    currentConfig.FiscalYearId = context.GetValue<int>(SDSDBQuery.Instance.GetFiscalYearId());
                }

                return currentConfig;
            }
        }

        public SDSBranchConfigEntity GetStoreConfig(string storeUniqueId)
        {
            var config = AllStoreConfigs.Find(p => p.UniqueId.ToLower() == storeUniqueId.ToLower());
            config.FiscalYearId = new DataContext().GetValue<int>(SDSDBQuery.Instance.GetFiscalYearId());
            return config;
        }

        public SDSBranchConfigEntity GetStoreConfig(int storeId)
        {
            var config = AllStoreConfigs.Find(p => p.CenterId == storeId);
            config.FiscalYearId = new DataContext().GetValue<int>(SDSDBQuery.Instance.GetFiscalYearId());
            return config;
        }
    }
}
