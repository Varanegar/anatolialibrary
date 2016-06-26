using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.ViewModels.Report
{
    public class DMCProductValueReportForPrintViewModel
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
        public decimal? SaleQty { set; get; }
        public decimal? SaleCarton { set; get; }
        public decimal? RetSaleQty { set; get; }
        public decimal? RetSaleCarton { set; get; }
        public decimal? SaleAmount { set; get; }
        public decimal? RetSaleAmount { set; get; }
        public decimal? SaleWeight { set; get; }
        public decimal? RetSaleWeight { set; get; }
        public decimal? SaleDiscount { set; get; }
        public decimal? RetSaleDiscount { set; get; }
        public int? SalePrizeCount { set; get; }
        public decimal? PrizeQty { set; get; }
        public decimal? PrizeCarton { set; get; }
    }
}
