using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.DMC.ViewModels.Report
{
    public class DMCProductReportForMapViewModel
    {
        public Guid UniqueId { get; set; }
        public string JDesc { get; set; }
        public string Lable { get; set; }
        public string Title { get; set; }
        public bool IsLeaf { get; set; }

    }

    public class DMCProductReportForPrintViewModel
    {
        public Guid UniqueId { get; set; }
        public string Lable { get; set; }
        public string Title { get; set; }
		public int?	OrderCount  { get; set; }
		public int?	SaleCount  { get; set; }
		public int?	RetSaleCount { get; set; } 
		public int?	SaleItemCount { get; set; } 
		public decimal?	RetSaleItemCount { get; set; } 
		public decimal?	SaleQty  { get; set; }
		public decimal?	SaleCarton { get; set; } 
		public decimal?	RetSaleQty  { get; set; }
		public decimal?	RetSaleCarton { get; set; } 
		public decimal?	SaleAmount  { get; set; }
		public decimal?	RetSaleAmount { get; set; } 
		public decimal?	SaleWeight  { get; set; }
		public decimal?	RetSaleWeight { get; set; } 
		public decimal?	SaleDiscount  { get; set; }
		public decimal?	RetSaleDiscount  { get; set; }
		public int?	SalePrizeCount  { get; set; }
		public decimal?	PrizeQty  { get; set; }
        public decimal? PrizeCarton { get; set; }

    }


}
