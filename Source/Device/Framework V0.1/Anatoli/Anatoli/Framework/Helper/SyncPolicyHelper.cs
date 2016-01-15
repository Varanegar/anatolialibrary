using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.App.Model.Product;
using Anatoli.App.Model.Store;

namespace Anatoli.Framework.Helper
{
    public enum SYNC_POLICY
    {
        InvalidData = 0,
        ForceOnline = 1,
        OnlineIfConnected = 2,
        OnlineIfWifi = 3,
        Offline
    }
    public class SyncPolicyHelper
    {
        private static SyncPolicyHelper Instance;

        public static SyncPolicyHelper GetInstance()
        {
            if (Instance == null)
                Instance = new SyncPolicyHelper();

            return Instance;
        }

        private SyncPolicyHelper()
        {

        }

        public SYNC_POLICY GetModelSyncPolicy(Type modelType)
        {
            if (modelType == typeof(CityRegionUpdateModel))
            {
                return SYNC_POLICY.OnlineIfConnected;
            }
            if (modelType == typeof(ProductPriceModel))
            {
                return SYNC_POLICY.OnlineIfConnected;
            }
            if (modelType == typeof(StoreUpdateModel))
            {
                return SYNC_POLICY.OnlineIfConnected;
            }
            if (modelType == typeof(ProductGroupModel))
            {
                return SYNC_POLICY.OnlineIfConnected;
            }
            if (modelType == typeof(ProductUpdateModel))
            {
                return SYNC_POLICY.OnlineIfConnected;
            }
            if (modelType == typeof(ProductModel))
            {
                return SYNC_POLICY.OnlineIfConnected;
            }
            else if (modelType == typeof(ProductImage))
            {
                return SYNC_POLICY.OnlineIfWifi;
            }
            return SYNC_POLICY.Offline;
        }
        public class SyncPolicyException : Exception
        {

        }
    }
}
