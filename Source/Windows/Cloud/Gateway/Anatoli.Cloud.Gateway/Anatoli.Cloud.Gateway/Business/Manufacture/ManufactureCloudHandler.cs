using Aantoli.Common.Entity.Gateway.Manufacture;
using Anatoli.Common.Gateway.Business.Manufacture;
using Anatoli.Framework.Busieness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Cloud.Gateway.Business.Manufacture
{
    public class ManufactureCloudHandler : ManufactureHandler
    {
        #region Singleton
        private static ManufactureCloudHandler Instance;
        public static ManufactureCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new ManufactureCloudHandler();

            return Instance;
        }
        private ManufactureCloudHandler()
            : base()
        {

        }
        #endregion

        public ManufactureListEntity GetSampleData()
        {
            ManufactureListEntity returnDataList = new ManufactureListEntity();
            ManufactureEntity returnData = new ManufactureEntity();
            returnData.ManufactureCode = 10001;
            returnData.ManufactureName = "شیرین عسل";
            returnDataList.Add(returnData);

            returnData = new ManufactureEntity();
            returnData.ManufactureCode = 10002;
            returnData.ManufactureName = "یک و یک";
            returnDataList.Add(returnData);

            returnData = new ManufactureEntity();
            returnData.ManufactureCode = 10003;
            returnData.ManufactureName = "میهن";
            returnDataList.Add(returnData);

            return returnDataList;
        }
    }
}
