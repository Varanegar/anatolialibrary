using System.Linq;
using Anatoli.DataAccess.Models;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using System;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ProductSimpleProxy : AnatoliProxy<Product, ProductViewModel>, IAnatoliProxy<Product, ProductViewModel>
    {

        #region Ctors
        public ProductSimpleProxy() 
        { }

        #endregion

        #region Methods
        public override ProductViewModel Convert(Product data)
        {
            var result = new ProductViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                ProductCode = data.ProductCode,
                Barcode = data.Barcode,
                StoreProductName = data.StoreProductName,
                ProductTypeId = data.ProductTypeId,
                QtyPerPack = data.QtyPerPack,
                IsRemoved = data.IsRemoved,

                ApplicationOwnerId = data.ApplicationOwnerId,

                ManufactureIdString = (data.ManufactureId == null) ? null : data.ManufactureId.ToString(),
                ManufactureName = (data.ManufactureId == null) ? string.Empty : data.Manufacture.ManufactureName,

                ProductGroupIdString = (data.ProductGroupId == null) ? null : data.ProductGroupId.ToString(),
                MainProductGroupIdString = (data.MainProductGroupId == null) ? null : data.MainProductGroupId.ToString(),

                MainSupplierId = (data.MainSupplierId == null) ? null : data.MainSupplierId.ToString(),
                MainSupplierName = (data.MainSupplierId == null) ? string.Empty : data.MainSupplier.SupplierName,

                IsActiveInOrder = data.IsActiveInOrder,
            };

            result.ProductTypeInfo = (data.ProductType == null) ? new ProductTypeViewModel() : new ProductTypeViewModel
            {
                ProductTypeName = data.ProductType.ProductTypeName,
                UniqueId = data.ProductType.Id
            };

            return result;

        }

        public override Product ReverseConvert(ProductViewModel data)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}