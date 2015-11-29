namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Store : BaseModel
    {
        //public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Lat { get; set; }
        public Nullable<decimal> Lng { get; set; }
        public Nullable<byte> HasDelivery { get; set; }
        public Nullable<int> GradeValueId { get; set; }
        public Nullable<int> StoreTemplateId { get; set; }
        public Nullable<byte> HasCourier { get; set; }
        public Nullable<byte> SupportAppOrder { get; set; }
        public Nullable<byte> SupportWebOrder { get; set; }
        public Nullable<byte> SupportCallCenterOrder { get; set; }
        public Nullable<byte> StoreStatusTypeId { get; set; }
    
        public virtual ICollection<StoreAction> StoreActions { get; set; }
        public virtual ICollection<StoreActiveOnHand> StoreActiveOnHands { get; set; }
        public virtual ICollection<StoreActivePriceList> StoreActivePriceLists { get; set; }
        public virtual ICollection<StoreCalendar> StoreCalendars { get; set; }
        public virtual ICollection<StoreDeliveryPerson> StoreDeliveryPersons { get; set; }
        public virtual ICollection<StoreValidRegionInfo> StoreValidRegionInfoes { get; set; }
    }
}
