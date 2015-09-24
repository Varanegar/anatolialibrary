using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Order
{
    public class PurchaseOrderItemEntity : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public Guid PriceId { get; set; }
        public decimal Qty { get; set; }
        public decimal Discount { get; set; }
        public decimal Add1 { get; set; }
        public decimal Add2 { get; set; }
        public decimal Amount { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsPrize { get; set; }
        public string Comment { get; set; }
        public bool AllowReplace { get; set; }
        public decimal Weight { get; set; }
    }
}
