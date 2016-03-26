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

                ManufactureId  = data.ManufactureId,
                ManufactureName = (data.ManufactureId == Guid.Empty) ? string.Empty : data.Manufacture.ManufactureName,

                ProductGroupId = data.ProductGroupId,
                MainProductGroupId = data.MainProductGroupId,

                MainSupplierId = data.MainSupplierId,
                MainSupplierName = (data.MainSupplierId == Guid.Empty) ? string.Empty : data.MainSupplier.SupplierName,

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