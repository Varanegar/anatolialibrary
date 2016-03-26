namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Customer : BaseModel
    {
        public Nullable<long> CustomerCode { get; set; }
        [StringLength(200)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        public Nullable<DateTime> BirthDay { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(500)]
        public string Email { get; set; }
        [StringLength(500)]
        public string MainStreet { get; set; }
        [StringLength(500)]
        public string OtherStreet { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        [StringLength(20)]
        public string NationalCode { get; set; }
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
        [ForeignKey("DefauleStore")]
        public Nullable<Guid> DefauleStoreId { get; set; }
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [ForeignKey("AnatoliAccount")]
        public Nullable<Guid> AnatoliAccountId { get; set; }
        public virtual AnatoliAccount AnatoliAccount { get; set; }

        public virtual ICollection<CustomerShipAddress> CustomerShipAddresses { get; set; }
        public virtual ICollection<Basket> CustomerBaskets { get; set; }
        public virtual ICollection<IncompletePurchaseOrder> IncompletePurchaseOrders { get; set; }
        public virtual Store DefauleStore { get; set; }
        public virtual CityRegion RegionInfo { get; set; }
        public virtual CityRegion RegionLevel1 { get; set; }
        public virtual CityRegion RegionLevel2 { get; set; }
        public virtual CityRegion RegionLevel3 { get; set; }
        public virtual CityRegion RegionLevel4 { get; set; }


    }
}
