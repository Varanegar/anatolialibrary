namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;

    public class Store : BaseModel
    {
        //public Guid StoreId { get; set; }
        public int StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public long Lat { get; set; }
        public long Lng { get; set; }
        public byte HasDelivery { get; set; }
        public Nullable<int> GradeValueId { get; set; }
        public Nullable<int> StoreTemplateId { get; set; }
        public byte HasCourier { get; set; }
        public byte SupportAppOrder { get; set; }
        public byte SupportWebOrder { get; set; }
        public byte SupportCallCenterOrder { get; set; }
        public Nullable<byte> StoreStatusTypeId { get; set; }

        public virtual ICollection<IncompletePurchaseOrder> IncompletePurchaseOrders { get; set; }
        public virtual ICollection<Stock> StoreStocks { get; set; }
        public virtual ICollection<StoreAction> StoreActions { get; set; }
        public virtual ICollection<StoreActiveOnhand> StoreActiveOnhands { get; set; }
        public virtual ICollection<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public virtual ICollection<StoreCalendar> StoreCalendars { get; set; }
        public virtual ICollection<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
        public virtual ICollection<CityRegion> StoreValidRegionInfoes { get; set; }
    }
}
