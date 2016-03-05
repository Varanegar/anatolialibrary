using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ProductCompleteInfoProxy : AnatoliProxy<Product, ProductViewModel>, IAnatoliProxy<Product, ProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<Manufacture, ManufactureViewModel> ManufactureProxy { get; set; }
        public IAnatoliProxy<ProductGroup, ProductGroupViewModel> ProductGroupProxy { get; set; }
        public IAnatoliProxy<MainProductGroup, MainProductGroupViewModel> MainProductGroupProxy { get; set; }
        public IAnatoliProxy<Supplier, SupplierViewModel> SupplierProxy { get; set; }
        public IAnatoliProxy<CharValue, CharValueViewModel> CharValueProxy { get; set; }

        #endregion

        #region Ctors
        public ProductCompleteInfoProxy() :
            this(AnatoliProxy<Manufacture, ManufactureViewModel>.Create(),
                 AnatoliProxy<MainProductGroup, MainProductGroupViewModel>.Create(),
                 AnatoliProxy<ProductGroup, ProductGroupViewModel>.Create(),
                 AnatoliProxy<CharValue, CharValueViewModel>.Create(),
                 AnatoliProxy<Supplier, SupplierViewModel>.Create()
            )
        { }

        public ProductCompleteInfoProxy(IAnatoliProxy<Manufacture, ManufactureViewModel> manufactureProxy,
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
            return new ProductViewModel
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
                IsRemoved = data.IsRemoved,

                PrivateOwnerId = data.PrivateLabelOwner.Id,

                ManufactureIdString  = (data.ManufactureId == null) ? null : data.ManufactureId.ToString(),
                ProductGroupIdString = (data.ProductGroupId == null) ? null : data.ProductGroupId.ToString(),
                MainProductGroupIdString = (data.MainProductGroupId == null) ? null : data.MainProductGroupId.ToString(),
                MainSupplierId = (data.MainSupplierId == null) ? null : data.MainSupplierId.ToString(),
                Suppliers = SupplierProxy.Convert(data.Suppliers.ToList()),
                CharValues = CharValueProxy.Convert(data.CharValues.ToList()),
            };
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
                ProductTypeId = data.ProductTypeId,
                IsRemoved = data.IsRemoved,

                PrivateLabelOwner = new Principal { Id = data.PrivateOwnerId },
                Manufacture = (data.ManufactureIdString == null) ? null : ManufactureProxy.ReverseConvert(data.ManufactureIdString, data.PrivateOwnerId),
                ProductGroup = (data.ProductGroupIdString == null) ? null : ProductGroupProxy.ReverseConvert(data.ProductGroupIdString, data.PrivateOwnerId),
                MainProductGroup = (data.MainProductGroupIdString == null) ? null : MainProductGroupProxy.ReverseConvert(data.MainProductGroupIdString, data.PrivateOwnerId),
                Suppliers = (data.Suppliers == null) ? null : SupplierProxy.ReverseConvert(data.Suppliers.ToList()),
                CharValues = (data.CharValues == null) ? null : CharValueProxy.ReverseConvert(data.CharValues.ToList()),

            };
        }
        #endregion
    }
}