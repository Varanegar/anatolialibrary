using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ProductProxy : AnatoliProxy<Product, ProductViewModel>, IAnatoliProxy<Product, ProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<Manufacture, ManufactureViewModel> ManufactureProxy { get; set; }
        public IAnatoliProxy<ProductGroup, ProductGroupViewModel> ProductGroupProxy { get; set; }
        public IAnatoliProxy<MainProductGroup, MainProductGroupViewModel> MainProductGroupProxy { get; set; }
        public IAnatoliProxy<Supplier, SupplierViewModel> SupplierProxy { get; set; }
        public IAnatoliProxy<CharValue, CharValueViewModel> CharValueProxy { get; set; }

        #endregion

        #region Ctors
        public ProductProxy() :
            this(AnatoliProxy<Manufacture, ManufactureViewModel>.Create(),
                 AnatoliProxy<MainProductGroup, MainProductGroupViewModel>.Create(),
                 AnatoliProxy<ProductGroup, ProductGroupViewModel>.Create(),
                 AnatoliProxy<CharValue, CharValueViewModel>.Create(),
                 AnatoliProxy<Supplier, SupplierViewModel>.Create()
            )
        { }

        public ProductProxy(IAnatoliProxy<Manufacture, ManufactureViewModel> manufactureProxy,
                            IAnatoliProxy<MainProductGroup, MainProductGroupViewModel> mainProductGroupProxy,
                            IAnatoliProxy<ProductGroup, ProductGroupViewModel> productGroupProxy,
                            IAnatoliProxy<CharValue, CharValueViewModel> charValueProxy,
                            IAnatoliProxy<Supplier, SupplierViewModel> supplierProxy
            )
        {
            ManufactureProxy = manufactureProxy;
            ProductGroupProxy = productGroupProxy;
            MainProductGroupProxy = mainProductGroupProxy;
            SupplierProxy = supplierProxy;
            CharValueProxy = charValueProxy;
        }
        #endregion

        #region Methods
        public override ProductViewModel Convert(Product data)
        {
            var result = new ProductViewModel
            {
                ID = data.Number_ID,
                UniqueId = data.Id,
                Desctription = data.Desctription,
                PackVolume = data.PackVolume,
                PackWeight = data.PackWeight,
                ProductCode = data.ProductCode,
                ProductName = data.ProductName,
                StoreProductName = data.StoreProductName,
                ProductTypeId = data.ProductTypeId,
                QtyPerPack = data.QtyPerPack,
                IsRemoved = data.IsRemoved,

                PrivateOwnerId = data.PrivateLabelOwner_Id,

                ManufactureIdString = (data.ManufactureId == null) ? null : data.ManufactureId.ToString(),
                ManufactureName = (data.ManufactureId == null) ? string.Empty : data.Manufacture.ManufactureName,

                ProductGroupIdString = (data.ProductGroupId == null) ? null : data.ProductGroupId.ToString(),
                MainProductGroupIdString = (data.MainProductGroupId == null) ? null : data.MainProductGroupId.ToString(),

                MainSupplierId = (data.MainSupplierId == null) ? null : data.MainSupplierId.ToString(),
                MainSupplierName = (data.MainSupplierId == null) ? string.Empty : data.MainSupplier.SupplierName,
            };

            result.ProductTypeInfo = (result.ProductTypeInfo == null) ? new ProductTypeViewModel() : new ProductTypeViewModel
            {
                ProductTypeName = data.ProductType.ProductTypeName,
                UniqueId = data.ProductType.Id
            };

            return result;

        }

        public override Product ReverseConvert(ProductViewModel data)
        {
            return new Product
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                Desctription = data.Desctription,
                PackVolume = data.PackVolume,
                PackWeight = data.PackWeight,
                ProductCode = data.ProductCode,
                ProductName = data.ProductName,
                StoreProductName = data.StoreProductName,
                QtyPerPack = (data.QtyPerPack == 0) ? 1 : data.QtyPerPack,
                IsRemoved = data.IsRemoved,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
                Manufacture = (data.ManufactureIdString == null) ? null : ManufactureProxy.ReverseConvert(data.ManufactureIdString, data.PrivateOwnerId),
                ProductGroup = (data.ProductGroupIdString == null) ? null : ProductGroupProxy.ReverseConvert(data.ProductGroupIdString, data.PrivateOwnerId),
                MainProductGroup = (data.MainProductGroupIdString == null) ? null : MainProductGroupProxy.ReverseConvert(data.MainProductGroupIdString, data.PrivateOwnerId),
                Suppliers = (data.Suppliers == null) ? null : SupplierProxy.ReverseConvert(data.Suppliers.ToList()),
                CharValues = (data.CharValues == null) ? null : CharValueProxy.ReverseConvert(data.CharValues.ToList()),

                ProductType = data.ProductTypeInfo == null ? new ProductType() : new ProductType { Id = data.ProductTypeInfo.UniqueId }
            };
        }
        #endregion
    }
}