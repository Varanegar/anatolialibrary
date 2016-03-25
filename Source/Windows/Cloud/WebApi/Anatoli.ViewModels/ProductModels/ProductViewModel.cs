using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductViewModel : BaseViewModel
    {
        public string Barcode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public decimal? PackVolume { get; set; }
        public decimal? PackWeight { get; set; }
        public decimal QtyPerPack { get; set; }
        public decimal RateValue { get; set; }
        public string Desctription { get; set; }

        public Guid PackUnitId { get; set; }
        public Guid? ProductTypeId { get; set; }
        public Guid TaxCategoryId { get; set; }

        public List<SupplierViewModel> Suppliers { get; set; }
        public Guid? MainProductGroupId { get; set; }
        public Guid? MainSupplierId { get; set; }

        public Guid? ProductGroupId { get; set; }
        public Guid? ManufactureId { get; set; }

        //public bool IsRemoved { get; set; }
        public List<CharValueViewModel> CharValues { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }

        public ProductTypeViewModel ProductTypeInfo { get; set; }
        public string MainSupplierName { get; set; }
        public string ManufactureName { get; set; }
        public bool IsActiveInOrder { get; set; }
    }
    
}
