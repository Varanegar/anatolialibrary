using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Anatoli.Common
{
    public static class UriInfo
    {
        public static readonly string SaveSupplierURI = "/api/gateway/base/supplier/save";
        public static readonly string SaveManufactureURI = "/api/gateway/base/manufacture/save";
        public static readonly string SaveCityRegionURI = "/api/gateway/base/region/save";
        public static readonly string SaveCharGroupURI = "/api/gateway/product/chargroups/save";
        public static readonly string SaveCharTypeURI = "/api/gateway/product/chartypes/save";
        public static readonly string SaveProductURI = "/api/gateway/product/save";
        public static readonly string SaveProductGroupURI = "/api/gateway/product/productgroups/save";
        public static readonly string SaveStoreURI = "/api/gateway/store/save";
        public static readonly string SaveStorePriceListURI = "/api/gateway/store/storepricelist/save";
        public static readonly string SaveStoreOnHandURI = "/api/gateway/store/storeOnhand/save";
        public static readonly string SaveImageURI = "/api/imageManager/Save";
        public static readonly string SaveStockURI = "/api/gateway/stock/save";
        public static readonly string SaveFiscalYearURI = "/api/gateway/fiscalyear/save";
        public static readonly string SaveStockOnHandURI = "/api/gateway/stock/stockOnhand/save";
        public static readonly string SaveStockProductURI = "/api/gateway/stock/stockproduct/save";
        public static readonly string SaveStockProductRequestURI = "/api/gateway/stock/stockproduct/save";
        public static readonly string SaveMainProductGroupURI = "/api/gateway/product/mainproductgroups/save";

        public static readonly string GetProductRequestRulesURI = "/api/gateway/stockproductrequest/rules/valid";
        public static readonly string GetProductsURI = "/api/gateway/product/products";
        public static readonly string GetMainProductGroupsURI = "/api/gateway/product/mainproductgroups";
        public static readonly string GetStocksURI = "/api/gateway/stock/stocks";
        public static readonly string GetStocksCompleteURI = "/api/gateway/stock/stocks/complete";
        public static readonly string GetStockRequestTypesURI = "/api/gateway/stock/stockrequesttypes";

    }
}
    