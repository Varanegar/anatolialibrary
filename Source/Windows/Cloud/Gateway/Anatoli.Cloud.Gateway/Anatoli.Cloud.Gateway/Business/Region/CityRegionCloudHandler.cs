using Aantoli.Common.Entity.Gateway.Region;
using Anatoli.Common.Gateway.Business.Region;
using Anatoli.Framework.Busieness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Cloud.Gateway.Business.Region
{
    public class CityRegionCloudHandler : CityRegionHandler
    {
        #region Singleton
        private static CityRegionCloudHandler Instance;
        public static CityRegionCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new CityRegionCloudHandler();

            return Instance;
        }
        private CityRegionCloudHandler()
            : base()
        {

        }
        #endregion

        public CityRegionListEntity GetSampleData()
        {
            CityRegionListEntity returnDataList = new CityRegionListEntity();
            CityRegionEntity returnData = new CityRegionEntity();
            returnData.CityId = Guid.NewGuid();
            returnData.ProvinceId = Guid.NewGuid();
            returnData.ZoneId = Guid.NewGuid();
            returnDataList.Add(returnData);

            returnData = new CityRegionEntity();
            returnData.CityId = Guid.NewGuid();
            returnData.ProvinceId = Guid.NewGuid();
            returnData.ZoneId = Guid.NewGuid();
            returnDataList.Add(returnData);

            returnData = new CityRegionEntity();
            returnData.CityId = Guid.NewGuid();
            returnData.ProvinceId = Guid.NewGuid();
            returnData.ZoneId = Guid.NewGuid();
            returnDataList.Add(returnData);

            return returnDataList;
        }

    }
}
