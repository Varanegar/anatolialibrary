using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Store
{
    public class StoreProductPriceEntity : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid PriceId { get; set; }
        public decimal Price { get; set; }
    }
}
