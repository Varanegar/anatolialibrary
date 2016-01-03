using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.ViewModels.EVC
{
    public class PMCEvcDetailViewModel : PMCBaseViewModel
    {
        public int EvcDetailId { get; set; }
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public double Qty { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountAmountX { get; set; }
        public int TaxCategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal AmountCalcBase { get; set; }
        public decimal NetAmount { get; set; }
        public int EVCID { get; set; }
        [Ignore]
        public int DetailId { get; set; }
        public bool IsPrize { get; set; }
    }
}
