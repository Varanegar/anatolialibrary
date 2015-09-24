using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Store
{
    public class StoreProductPriceListEntity : BaseEntity
    {
        public Guid StoreId { get; set; }
        public DateTime LastUpdate { get; set; }
        public List<StoreProductPriceEntity> PriceListInfo { get; set; }
    }
}
