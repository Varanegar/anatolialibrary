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
    public class ProductGroupCloudHandler : ProductGroupHandler
    {
                #region Singleton
        private static ProductGroupCloudHandler Instance;
        public static ProductGroupCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new ProductGroupCloudHandler();

            return Instance;
        }
        private ProductGroupCloudHandler()
            : base()
        {

        }
        #endregion

        public ProductGroupListEntity GetSampleData()
        {
            ProductGroupListEntity returnDataList = new ProductGroupListEntity();
            ProductGroupEntity returnData = new ProductGroupEntity();
            returnData.CharGroupId = Guid.NewGuid();
            returnData.NLeft = 1001;
            returnData.NRight = 2001;
            returnData.ParentId = 0;
            returnData.ParentUniqueId = Guid.Empty;
            returnData.GroupName = "موبایل";
            returnData.ID = 1;
            returnDataList.Add(returnData);

            returnData = new ProductGroupEntity();
            returnData.CharGroupId = Guid.NewGuid();
            returnData.NLeft = 1001;
            returnData.NRight = 2001;
            returnData.ParentId = 0;
            returnData.ParentUniqueId = Guid.Empty;
            returnData.GroupName = "شیرها";
            returnData.ID = 1;
            returnDataList.Add(returnData);

            return returnDataList;
        }

    }
}
