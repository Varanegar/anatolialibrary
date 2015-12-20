using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.ViewModels.ProductModels
{
    public class ProductViewModel : BaseViewModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StoreProductName { get; set; }
        public decimal? PackVolume { get; set; }
        public decimal? PackWeight { get; set; }
        public decimal RateValue { get; set; }
        public string SmallPicURL { get; set; }
        public string LargePicURL { get; set; }
        public string Desctription { get; set; }

        public Guid PackUnitId { get; set; }
        public Guid ProductTypeId { get; set; }
        public Guid TaxCategoryId { get; set; }

        public List<SupplierViewModel> Suppliers { get; set; }
        public ProductGroupViewModel ProductGroup { get; set; }
        public ManufactureViewModel Manufacture { get; set; }
    }
    
}
