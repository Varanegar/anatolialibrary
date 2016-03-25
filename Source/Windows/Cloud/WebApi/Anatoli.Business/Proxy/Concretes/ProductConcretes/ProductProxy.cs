using System.Linq;
using Anatoli.DataAccess.Models;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.StockModels;
using System;

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
                Barcode = data.Barcode,
                ProductName = data.ProductName,
                StoreProductName = data.StoreProductName,
                ProductTypeId = data.ProductTypeId,
                QtyPerPack = data.QtyPerPack,
                IsRemoved = data.IsRemoved,
                ManufactureId = data.ManufactureId,

                ProductGroupId = data.ProductGroupId,
                MainProductGroupId = data.MainProductGroupId,

                MainSupplierId = data.MainSupplierId,

                IsActiveInOrder = data.IsActiveInOrder,
            };


            return result;

        }

        public override Product ReverseConvert(ProductViewModel data)
        {
            
            Product result =  new Product
            {
                Number_ID = data.ID,
                Id = data.UniqueId,
                Desctription = data.Desctription,
                PackVolume = data.PackVolume,
                PackWeight = data.PackWeight,
                Barcode = data.Barcode,
                ProductCode = data.ProductCode,
                ProductName = data.ProductName,
                StoreProductName = data.StoreProductName,
                QtyPerPack = (data.QtyPerPack == 0) ? 1 : data.QtyPerPack,
                IsRemoved = data.IsRemoved,

                ApplicationOwnerId = data.ApplicationOwnerId,
                //Suppliers = (data.Suppliers == null) ? null : SupplierProxy.ReverseConvert(data.Suppliers.ToList()),
                //CharValues = (data.CharValues == null) ? null : CharValueProxy.ReverseConvert(data.CharValues.ToList()),

                ProductTypeId = data.ProductTypeId
            };

            if (data.ManufactureId == Guid.Empty) result.ManufactureId = null; else result.ManufactureId = data.ManufactureId;
            if (data.ProductGroupId == Guid.Empty) result.ProductGroupId = null; else result.ProductGroupId = data.ProductGroupId;
            if (data.MainProductGroupId == Guid.Empty) result.MainProductGroupId = null; else result.MainProductGroupId = data.MainProductGroupId;
            if (data.MainSupplierId == Guid.Empty) result.MainSupplierId = null; else result.MainSupplierId = data.MainSupplierId;

            return result;
        }
        #endregion
    }
}