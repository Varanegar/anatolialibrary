namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class CustomerShipAddress : BaseModel
    {
        //public Guid CustomerShipAddressId { get; set; }
        [StringLength(100)]
        public string AddressName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<long> CityId { get; set; }
        public Nullable<long> ZoneId { get; set; }
        [StringLength(50)]
        public string MainStreet { get; set; }
        [StringLength(50)]
        public string OtherStreet { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        public Nullable<byte> IsDefault { get; set; }
        public Nullable<int> DefauleStoreId { get; set; }
        public Nullable<long> Lat { get; set; }
        public Nullable<long> Lng { get; set; }
        //public Nullable<Guid> CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
