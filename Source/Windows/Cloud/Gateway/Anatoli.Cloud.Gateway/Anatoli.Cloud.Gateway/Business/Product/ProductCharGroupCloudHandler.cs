using Aantoli.Common.Entity.Gateway.Product;
using Anatoli.Common.Gateway.Business.Region;
using Anatoli.Framework.Busieness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Cloud.Gateway.Business.Region
{
    public class ProductCharGroupCloudHandler : ProductCharGroupHandler
    {
        #region Singleton
        private static ProductCharGroupCloudHandler Instance;
        public static ProductCharGroupCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new ProductCharGroupCloudHandler();

            return Instance;
        }
        private ProductCharGroupCloudHandler()
            : base()
        {

        }
        #endregion

        public ProductCharGroupListEntity GetSampleData()
        {
            ProductCharGroupListEntity returnDataList = new ProductCharGroupListEntity();
            ProductCharGroupEntity returnData = new ProductCharGroupEntity();
            returnData.GroupCode = 100001;
            returnData.GroupName = "موبایل";
            returnData.ID = 1;
            ProductCharTypeListEntity infoList = new ProductCharTypeListEntity();
            ProductCharTypeEntity info = new ProductCharTypeEntity();
            info.ID = 1;
            info.CharTypeDesc = "نوع مویابل";
            infoList.Add(info);
            info = new ProductCharTypeEntity();
            info.ID = 2;
            info.CharTypeDesc = "میزان حافظه";
            infoList.Add(info);

            returnData.GroupTypeList = infoList;
            returnDataList.Add(returnData);

            returnData = new ProductCharGroupEntity();
            returnData.GroupCode = 100002;
            returnData.GroupName = "لبنی";
            returnData.ID = 2;
            infoList = new ProductCharTypeListEntity();
            info = new ProductCharTypeEntity();
            info.ID = 3;
            info.CharTypeDesc = "میزان چربی";
            infoList.Add(info);
            info = new ProductCharTypeEntity();
            info.ID = 4;
            info.CharTypeDesc = "ماندگاری";
            infoList.Add(info);

            returnData.GroupTypeList = infoList;
            returnDataList.Add(returnData);

            return returnDataList;
        }
    }
}
