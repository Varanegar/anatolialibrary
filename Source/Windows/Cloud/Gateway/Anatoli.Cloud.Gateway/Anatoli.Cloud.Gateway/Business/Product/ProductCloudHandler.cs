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
    public class ProductCloudHandler : ProductHandler
    {
        #region Singleton
        private static ProductCloudHandler Instance;
        public static ProductCloudHandler GetInstance()
        {
            if (Instance == null)
                Instance = new ProductCloudHandler();

            return Instance;
        }
        private ProductCloudHandler()
            : base()
        {

        }
        #endregion

        public ProductListEntity GetSampleData()
        {
            ProductListEntity returnDataList = new ProductListEntity();
            ProductEntity returnData = new ProductEntity();
            returnData.ProductCode = 100001;
            returnData.ProductName = "ماست پرچرب";
            returnData.StoreProductName = "ماست پرچرب ویژه میهن";
            returnData.PackUnitId = Guid.NewGuid();
            returnData.ProductTypeId = Guid.NewGuid();
            returnData.PackVolume = Convert.ToDecimal(14.9);
            returnData.PackWeight = 43;
            returnData.TaxCategoryId = Guid.NewGuid();
            returnData.ProductGroupId = Guid.NewGuid();
            returnData.ManufactureId = Guid.NewGuid();
            returnData.RateValue = Convert.ToDecimal(4.5);
            returnData.SmallPicURL = "";
            returnData.LargePicURL = "";
            returnData.ID = 1;
            ProductSupplierListEntity infoList = new ProductSupplierListEntity();
            ProductSupplierEntity info = new ProductSupplierEntity();
            info.ID = 1;
            info.SupplierId = Guid.NewGuid();
            info.SupplierComment = "";
            infoList.Add(info);

            info = new ProductSupplierEntity();
            info.ID = 2;
            info.SupplierId = Guid.NewGuid();
            infoList.Add(info);

            returnData.SupplierInfoList = infoList;
            returnDataList.Add(returnData);

            returnData = new ProductEntity();
            returnData.ProductCode = 100002;
            returnData.ProductName = "شیر مدت دار 1.5 لیتری";
            returnData.StoreProductName = "";
            returnData.PackUnitId = Guid.NewGuid();
            returnData.ProductTypeId = Guid.NewGuid();
            returnData.PackVolume = Convert.ToDecimal(14.9);
            returnData.PackWeight = 12;
            returnData.TaxCategoryId = Guid.NewGuid();
            returnData.ProductGroupId = Guid.NewGuid();
            returnData.ManufactureId = Guid.NewGuid();
            returnData.RateValue = Convert.ToDecimal(2.73);
            returnData.SmallPicURL = "";
            returnData.LargePicURL = "";
            returnData.ID = 2;
            infoList = new ProductSupplierListEntity();
            info = new ProductSupplierEntity();
            info.ID = 3;
            info.SupplierId = Guid.NewGuid();
            info.SupplierComment = "";
            infoList.Add(info);

            info = new ProductSupplierEntity();
            info.ID = 4;
            info.SupplierId = Guid.NewGuid();
            infoList.Add(info);

            return returnDataList;
        }
    }
}
