using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.ViewModels.EVC
{
    public class PMCEvcViewModel : PMCBaseViewModel
    {
        public int EVCId { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountAmount2 { get; set; }
        public decimal DiscountAmountX2 { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Amount { get; set; }
        public int? CustomerId { get; set; }
        public int? SalesmanId { get; set; }
        public int CenterId { get; set; }
        public string DateOf { get; set; }
        [Ignore]
        public List<PMCEvcDetailViewModel> EVCDetail { get; set; }
    }
}
