using System;
using System.Linq;
using Anatoli.DataAccess.Models;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models.Identity;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.Business.Proxy.ProductConcretes
{
    public class ProductProxy : AnatoliProxy<Product, ProductViewModel>, IAnatoliProxy<Product, ProductViewModel>
    {
        #region Properties
        public IAnatoliProxy<Manufacture, ManufactureViewModel> ManufactureProxy { get; set; }
        public IAnatoliProxy<ProductGroup, ProductGroupViewModel> ProductGroupProxy { get; set; }
        public IAnatoliProxy<Supplier, SupplierViewModel> SupplierProxy { get; set; }

        #endregion

        #region Ctors
        public ProductProxy() :
            this(AnatoliProxy<Manufacture, ManufactureViewModel>.Create(),
                 AnatoliProxy<ProductGroup, ProductGroupViewModel>.Create(),
                 AnatoliProxy<Supplier, SupplierViewModel>.Create()
            )
        { }

        public ProductProxy(IAnatoliProxy<Manufacture, ManufactureViewModel> manufactureProxy,
                            IAnatoliProxy<ProductGroup, ProductGroupViewModel> productGroupProxy,
                            IAnatoliProxy<Supplier, SupplierViewModel> supplierProxy
            )
        {
            ManufactureProxy = manufactureProxy;
            ProductGroupProxy = productGroupProxy;
            SupplierProxy = supplierProxy;
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
                LargePicURL = "",
                SmallPicURL = "",
                PackVolume = data.PackVolume,
                PackWeight = data.PackWeight,
                ProductCode = data.ProductCode,
                ProductName = data.ProductName,
                StoreProductName = data.StoreProductName,

                //PackUnitId=data.PackUnitValueId
                //ProductTypeId=data.ProductTypeValueId,
                //RateValue=data.ProductRates,
                //TaxCategoryId=data.TaxCategoryValueId,

                PrivateLabelKey = data.PrivateLabelOwner.Id,

                ManufactureIdString  = (data.Manufacture == null) ? null : data.Manufacture.Id.ToString(),
                ProductGroupIdString  = (data.ProductGroup == null) ? null : data.ProductGroup.Id.ToString(),
                Suppliers = (data.Suppliers == null) ? null : SupplierProxy.Convert(data.Suppliers.ToList()),
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

                PrivateLabelOwner = new Principal { Id = data.PrivateLabelKey },
                Manufacture = (data.ManufactureIdString == null) ? null : ManufactureProxy.ReverseConvert(data.ManufactureIdString, data.PrivateLabelKey),
                ProductGroup = (data.ProductGroupIdString == null) ? null : ProductGroupProxy.ReverseConvert(data.ProductGroupIdString, data.PrivateLabelKey),
                Suppliers = (data.Suppliers == null) ? null : SupplierProxy.ReverseConvert(data.Suppliers.ToList()),
            };
        }
        #endregion
    }
}