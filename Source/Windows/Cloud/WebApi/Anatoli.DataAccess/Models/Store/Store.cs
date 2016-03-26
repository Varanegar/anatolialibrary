namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Store : BaseModel
    {
        public int StoreCode { get; set; }
        [StringLength(100)]
        public string StoreName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Phone { get; set; }
        public long Lat { get; set; }
        public long Lng { get; set; }
        public byte HasDelivery { get; set; }
        public byte HasCourier { get; set; }
        public byte SupportAppOrder { get; set; }
        public byte SupportWebOrder { get; set; }
        public byte SupportCallCenterOrder { get; set; }
        public virtual ICollection<IncompletePurchaseOrder> IncompletePurchaseOrders { get; set; }
        public virtual ICollection<Stock> StoreStocks { get; set; }
        public virtual ICollection<StoreAction> StoreActions { get; set; }
        public virtual ICollection<StoreActiveOnhand> StoreActiveOnhands { get; set; }
        public virtual ICollection<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public virtual ICollection<StoreCalendar> StoreCalendars { get; set; }
        public virtual ICollection<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
        public virtual ICollection<CityRegion> StoreValidRegionInfoes { get; set; }
        [ForeignKey("DistCompanyCenter")]
        public Nullable<Guid> DistCompanyCenterId { get; set; }
        public virtual DistCompanyCenter DistCompanyCenter { get; set; }

        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
