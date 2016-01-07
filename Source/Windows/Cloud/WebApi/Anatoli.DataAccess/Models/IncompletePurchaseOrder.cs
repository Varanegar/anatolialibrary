namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class IncompletePurchaseOrder : BaseModel
    {
        [ForeignKey("Store")]
        public Guid StoreId { get; set; }
        [ForeignKey("CityRegion")]
        public Guid CityRegionId { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Guid DeliveryTypeId { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public string OrderShipAddress { get; set; }
        //تحویل گیرنده
        public string Transferee { get; set; }
        //تلفن تحویل گیرنده
        public string Phone { get; set; }
        public DateTime? DeliveryFromTime { get; set; }
        public DateTime? DeliveryToTime { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public ICollection<IncompletePurchaseOrderLineItem> IncompletePurchaseOrderLineItems { get; set; }
        public virtual CityRegion CityRegion { get; set; }
        public virtual Store Store { get; set; }
        public virtual Customer Customer{ get; set; }

    }
}
