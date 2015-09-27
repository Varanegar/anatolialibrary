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
    public class StoreProductPriceListsCloudHandler : StoreProductPriceListsHandler
    {
        #region Singleton
        private static StoreProductPriceListsCloudHandler Instance;
        public static StoreProductPriceListsCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new StoreProductPriceListsCloudHandler();

            return Instance;
        }
        private StoreProductPriceListsCloudHandler()
            : base()
        {

        }
        #endregion

        public StoreProductPriceListsListEntity GetSampleData()
        {
            StoreProductPriceListsListEntity returnDataList = new StoreProductPriceListsListEntity();
            StoreProductPriceListsEntity returnData = new StoreProductPriceListsEntity();
            returnData.UpdateDate = DateTime.Now;
            returnData.StoreId = Guid.NewGuid();
            returnData.ID = 1;
            StoreProductPriceListEntity infoList = new StoreProductPriceListEntity();
            StoreProductPriceEntity info = new StoreProductPriceEntity();
            info.ID = 1;
            info.ProductId = Guid.NewGuid();
            info.PriceId = Guid.NewGuid();
            info.Price = 12;
            infoList.Add(info);
            info = new StoreProductPriceEntity();
            info.ID = 2;
            info.ProductId = Guid.NewGuid();
            info.PriceId = Guid.NewGuid();
            info.Price = 658000;
            infoList.Add(info);

            returnData.PriceListInfo = infoList;
            returnDataList.Add(returnData);

            returnData.UpdateDate = DateTime.Now;
            returnData.StoreId = Guid.NewGuid();
            returnData.ID = 2;
            infoList = new StoreProductPriceListEntity();
            info = new StoreProductPriceEntity();
            info.ID = 3;
            info.ProductId = Guid.NewGuid();
            info.PriceId = Guid.NewGuid();
            info.Price = 729000;
            infoList.Add(info);
            info = new StoreProductPriceEntity();
            info.ID = 4;
            info.ProductId = Guid.NewGuid();
            info.PriceId = Guid.NewGuid();
            info.Price = 342790;
            infoList.Add(info);


            return returnDataList;
        }
    }
}
