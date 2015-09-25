using Aantoli.Common.Entity.Gateway.Store;
using Anatoli.Common.Gateway.Business.Store;
using Anatoli.Framework.Busieness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Cloud.Gateway.Business.Store
{
    public class StoreOnHandCloudHandler : StoreOnHandHandler
    {
        #region Singleton
        private static StoreOnHandCloudHandler Instance;
        public static StoreOnHandCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new StoreOnHandCloudHandler();

            return Instance;
        }
        private StoreOnHandCloudHandler()
            : base()
        {

        }
        #endregion

        public StoreOnHandListEntity GetSampleData()
        {
            StoreOnHandListEntity returnDataList = new StoreOnHandListEntity();
            StoreOnHandEntity returnData = new StoreOnHandEntity();
            returnData.UpdateDate = DateTime.Now;
            returnData.StoreId = Guid.NewGuid();
            returnData.OnHandQty = new Dictionary<Guid, decimal>();
            returnData.OnHandQty.Add(Guid.NewGuid(), 152);
            returnData.OnHandQty.Add(Guid.NewGuid(), 28454);
            returnData.OnHandQty.Add(Guid.NewGuid(), 12);
            returnDataList.Add(returnData);

            returnData = new StoreOnHandEntity();
            returnData.UpdateDate = DateTime.Now;
            returnData.StoreId = Guid.NewGuid();
            returnData.OnHandQty = new Dictionary<Guid, decimal>();
            returnData.OnHandQty.Add(Guid.NewGuid(), 23);
            returnData.OnHandQty.Add(Guid.NewGuid(), 2);
            returnDataList.Add(returnData);

            return returnDataList;
        }
    }
}
