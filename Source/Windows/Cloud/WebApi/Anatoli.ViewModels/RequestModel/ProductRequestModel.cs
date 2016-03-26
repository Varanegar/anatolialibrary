using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;

namespace Anatoli.ViewModels
{
    public class ProductRequestModel : BaseRequestModel
    {
        public List<ProductViewModel> productData { get; set; }
        public List<CharGroupViewModel> charGroupData { get; set; }
        public List<CharTypeViewModel> charTypeData { get; set; }
        public List<ProductTagViewModel> productTagData { get; set; }
        public List<ProductTagValueViewModel> productTagValueData { get; set; }
        public List<ProductGroupViewModel> productGroupData { get; set; }
        public List<ProductSupplierViewModel> productSupplierData { get; set; }
        public List<ProductCharValueViewModel> productCharData { get; set; }
        public List<MainProductGroupViewModel> mainProductGroupData { get; set; }
        public List<ProductRateViewModel> productRateData { get; set; }
        
    }
}
