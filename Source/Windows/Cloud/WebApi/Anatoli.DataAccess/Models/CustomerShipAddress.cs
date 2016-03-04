namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class CustomerShipAddress : BaseModel
    {
        [StringLength(100)]
        public string AddressName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [ForeignKey("RegionInfo"), Column(Order = 0)]
        public Nullable<Guid> RegionInfoId { get; set; }
        [ForeignKey("RegionLevel1"), Column(Order = 1)]
        public Nullable<Guid> RegionLevel1Id { get; set; }
        [ForeignKey("RegionLevel2"), Column(Order = 2)]
        public Nullable<Guid> RegionLevel2Id { get; set; }
        [ForeignKey("RegionLevel3"), Column(Order = 3)]
        public Nullable<Guid> RegionLevel3Id { get; set; }
        [ForeignKey("RegionLevel4"), Column(Order = 4)]
        public Nullable<Guid> RegionLevel4Id { get; set; }
        [StringLength(50)]
        public string MainStreet { get; set; }
        [StringLength(50)]
        public string OtherStreet { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        [StringLength(100)]
        public string Transferee { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        [ForeignKey("DefauleStore")]
        public Nullable<Guid> DefauleStore_Id { get; set; }
        public Nullable<decimal> Lat { get; set; }
        public Nullable<decimal> Lng { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Store DefauleStore { get; set; }
        public virtual CityRegion RegionInfo { get; set; }
        public virtual CityRegion RegionLevel1 { get; set; }
        public virtual CityRegion RegionLevel2 { get; set; }
        public virtual CityRegion RegionLevel3 { get; set; }
        public virtual CityRegion RegionLevel4 { get; set; }

        public virtual ICollection<IncompletePurchaseOrder> IncompletePurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }

    }
}
