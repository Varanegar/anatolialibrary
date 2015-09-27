using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anatoli.App.Model.Product;

namespace Anatoli.Framework.Helper
{
    public enum SYNC_POLICY
    {
        InvalidData = 0,
        ForceOnline = 1,
        ForceOnlineIfConnected = 2,
        ForceOnlineIfWifi = 3,
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
            if (modelType == typeof(ProductModel))
            {
                return SYNC_POLICY.Offline;
            }
            else if (modelType == typeof(ProductImage))
            {
                return SYNC_POLICY.ForceOnlineIfWifi;
            }
            return SYNC_POLICY.ForceOnline;
        }
    }
}
