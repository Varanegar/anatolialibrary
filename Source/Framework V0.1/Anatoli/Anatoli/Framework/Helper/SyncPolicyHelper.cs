using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.Helper
{
    public enum SYNC_POLICY
    {
        InvalidData = 0,
        ForceOnine = 1
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
            return SYNC_POLICY.ForceOnine;
        }
    }
}
