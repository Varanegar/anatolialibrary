using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aantoli.Common.Entity.Common;

namespace Aantoli.Common.Entity.Gateway.Order
{
    public class PurchaseBaseEntity : BaseEntity
    {
        public SOURCE_TYPE RequestSourceId { get; set; }
        public string DeviceIMEI { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomeId { get; set; }
        public Guid ShipAddressId { get; set; }
        public Guid StoreId { get; set; }
        public Guid OrderStatusId { get; set; }
        public List<OrderStatusInfoEntity> OrderStatusInfoList { get; set; }
    }
}
