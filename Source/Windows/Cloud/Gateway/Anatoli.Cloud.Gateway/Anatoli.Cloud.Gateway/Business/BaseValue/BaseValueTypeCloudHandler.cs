using Aantoli.Common.Entity.Gateway.BaseValue;
using Anatoli.Common.Gateway.Business.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Cloud.Gateway.Business.BaseValue
{
    public class BaseValueTypeCloudHandler : BaseValueTypeHandler
    {
        #region Singleton
        private static BaseValueTypeCloudHandler Instance;
        public static BaseValueTypeCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new BaseValueTypeCloudHandler();

            return Instance;
        }
        private BaseValueTypeCloudHandler()
            : base()
        {

        }
        #endregion

        public BaseValueTypeListEntity GetSampleData()
        {
            BaseValueTypeListEntity returnDataList = new BaseValueTypeListEntity();
            BaseValueTypeEntity returnData = new BaseValueTypeEntity();
            returnData.BaseTypeName = "واحد سنجش";
            returnData.BaseTypeDescription = "واحد سنجش کالا بر اساس وزن";
            returnData.ID = 1;
            BaseValueInfoListEntity infoList = new BaseValueInfoListEntity();
            BaseValueInfoEntity info = new BaseValueInfoEntity();
            info.ID = 1;
            info.BaseValueName = "کیلوگرم";
            infoList.Add(info);
            info = new BaseValueInfoEntity();
            info.ID = 2;
            info.BaseValueName = "گرم";
            infoList.Add(info);

            returnData.BaseValueList = infoList;
            returnDataList.Add(returnData);

            returnData = new BaseValueTypeEntity();
            returnData.BaseTypeName = "نوع کالا";
            returnData.BaseTypeDescription = "نوع کالا بر اساس انبار";
            returnData.ID = 2;
            infoList = new BaseValueInfoListEntity();
            info = new BaseValueInfoEntity();
            info.ID = 3;
            info.BaseValueName = "کالا";
            infoList.Add(info);
            info = new BaseValueInfoEntity();
            info.ID = 4;
            info.BaseValueName = "خدمات";
            infoList.Add(info);

            returnData.BaseValueList = infoList;
            returnDataList.Add(returnData);

            return returnDataList;
        }

    }
}
