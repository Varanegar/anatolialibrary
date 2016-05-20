namespace Anatoli.SDS.ViewModels.GisReport
{
    public class SDSProductReportViewModel
    {
        public double? Latitude { set; get; }
        public double? Longitude { set; get; }
        public string Title { set; get; }

        public int CustRef { set; get; }
        public int? OrderCount { set; get; }
        public int? SaleCount { set; get; }
        public int? RetSaleCount { set; get; }
        public int? SaleItemCount { set; get; }
        public int? RetSaleItemCount { set; get; }
        public double? SaleQty { set; get; }
        public double? SaleCarton { set; get; }
        public double? RetSaleQty { set; get; }
        public double? RetSaleCarton { set; get; }
        public decimal? SaleAmount { set; get; }
        public decimal? RetSaleAmount { set; get; }
        public double? SaleWeight { set; get; }
        public double? RetSaleWeight { set; get; }
        public decimal? SaleDiscount { set; get; }
        public decimal? RetSaleDiscount { set; get; }
        public int? SalePrizeCount { set; get; }
        public double? PrizeQty { set; get; }
        public double? PrizeCarton { set; get; }
    }
}
