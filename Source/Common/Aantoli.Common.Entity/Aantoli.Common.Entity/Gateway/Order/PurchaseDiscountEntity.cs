using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Order
{
    public class PurchaseDiscountEntity : BaseEntity
    {
        public string CardCode { get; set; }
        public string CardDesc { get; set; }
        public decimal Amount { get; set; }
    }
}
