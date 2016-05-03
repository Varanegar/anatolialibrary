using System;
using Anatoli.ViewModels.User;
using System.Collections.Generic;
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels.ProductModels;
using Anatoli.ViewModels.BaseModels;

namespace Anatoli.ViewModels
{
    public class GeneralRequestModel : BaseRequestModel
    {
        public List<BaseTypeViewModel> baseTypeData { get; set; }
        public List<FiscalYearViewModel> fiscalYearData { get; set; }
        public List<CityRegionViewModel> cityRegionData { get; set; }
        public List<SupplierViewModel> supplierData { get; set; }
        public List<BrandViewModel> brandData { get; set; }
        public List<ManufactureViewModel> manufactureData { get; set; }
        public List<StockProductRequestTypeViewModel> baseStockProductRequestType { get; set; }
    }
}
