using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.ViewModels.Order
{
    public class PMCSellDetailViewModel : PMCBaseViewModel
    {
        public int SellDetailId { get; set; }
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsPrize { get; set; }
        public double UnitQty { get; set; }
        [Ignore]
        public int PackQty { get; set; }
        public double? DiscountPercent { get; set; }
        public double? AddPercent { get; set; }
        public double Qty { get; set; }
        public double RequestQty { get; set; }
        public decimal Amount { get; set; }
    }
}
