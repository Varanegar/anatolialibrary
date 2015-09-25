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
    public class ProductCharTypeCloudHandler : ProductCharTypeHandler
    {
        #region Singleton
        private static ProductCharTypeCloudHandler Instance;
        public static ProductCharTypeCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new ProductCharTypeCloudHandler();

            return Instance;
        }
        private ProductCharTypeCloudHandler()
            : base()
        {

        }
        #endregion

        public ProductCharTypeListEntity GetSampleData()
        {
            ProductCharTypeListEntity returnDataList = new ProductCharTypeListEntity();
            ProductCharTypeEntity returnData = new ProductCharTypeEntity();
            returnData.CharTypeDesc = "نوع موبایل";
            returnData.ID = 1;
            ProductCharValueListEntity infoList = new ProductCharValueListEntity();
            ProductCharValueEntity info = new ProductCharValueEntity();
            info.ID = 1;
            info.CharValueText = "هوشمند";
            infoList.Add(info);
            info = new ProductCharValueEntity();
            info.ID = 2;
            info.CharValueText = "نیمه هوشمند";
            info = new ProductCharValueEntity();
            info.ID = 2;
            info.CharValueText = "خنگ";
            infoList.Add(info);
            returnData.CharValueList = infoList;
            returnDataList.Add(returnData);

            returnData = new ProductCharTypeEntity();
            returnData.CharTypeDesc = "سیستم عامل";
            returnData.ID = 2;
            infoList = new ProductCharValueListEntity();
            info = new ProductCharValueEntity();
            info.ID = 3;
            info.CharValueText = "ISO";
            infoList.Add(info);
            info = new ProductCharValueEntity();
            info.ID = 4;
            info.CharValueText = "Android";
            infoList.Add(info);
            returnData.CharValueList = infoList;
            returnDataList.Add(returnData);


            returnData = new ProductCharTypeEntity();
            returnData.CharTypeDesc = "میزان حافظه";
            returnData.ID = 2;
            infoList = new ProductCharValueListEntity();
            info = new ProductCharValueEntity();
            info.ID = 3;
            info.CharValueFromAmount = 0;
            info.CharValueToAmount = 32;
            info.CharValueText = "کم";
            infoList.Add(info);
            info = new ProductCharValueEntity();
            info.ID = 4;
            info.CharValueFromAmount = 33;
            info.CharValueToAmount = 128;
            info.CharValueText = "متوسط";
            infoList.Add(info);
            returnData.CharValueList = infoList;
            returnDataList.Add(returnData);

            return returnDataList;
        }

    }
}
