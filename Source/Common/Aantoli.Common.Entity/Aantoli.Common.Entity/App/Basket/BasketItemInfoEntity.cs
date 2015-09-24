using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.App.Basket
{
    public class BasketItemInfoEntity : BaseEntity
    {
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
    }
}
