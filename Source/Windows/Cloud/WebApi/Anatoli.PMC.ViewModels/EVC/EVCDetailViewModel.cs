using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.PMC.ViewModels.EVC
{
    public class EVCDetailViewModel : PMCBaseViewModel
    {
        public int EvcDetailId { get; set; }
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public double Qty { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal AmountCalcBase { get; set; }
        public decimal NetAmount { get; set; }
        public int EVCID { get; set; }
        public int DetailId { get; set; }
        public bool IsPrize { get; set; }
    }
}
