using Aantoli.Framework.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aantoli.Common.Entity.Common;

namespace Aantoli.Common.Entity.Gateway.Order
{
    public class PurchaseOrderEntity : PurchaseBaseEntity
    {
        //Payment Info
        public Guid PaymentTypeId { get; set; }
        public decimal PaidAmount { get; set; }
        public Guid PaidAccountId { get; set; }
        public string TrackingNo { get; set; }

        public List<PurchaseDiscountEntity> GiftCardInfoList { get; set; }
        public List<PurchaseDiscountEntity> DiscountCodeList { get; set; }

        //Order Info
        public string AppOrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Discount { get; set; }
        public decimal Add1 { get; set; }
        public decimal Add2 { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }

        //Delivery Info
        public Guid DeliveryTypeId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public TimeSpan DeliveryFromTime { get; set; }
        public TimeSpan DeliveryToTime { get; set; }
        public List<PurchaseOrderItemEntity> ItemInfoList { get; set; }
    }
}
