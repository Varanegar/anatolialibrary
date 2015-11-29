namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CustomerShipAddress : BaseModel
    {
        //public Guid CustomerShipAddressId { get; set; }
        public string AddressName { get; set; }
        public string Phone { get; set; }
        public Nullable<long> Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<long> CityId { get; set; }
        public Nullable<long> ZoneId { get; set; }
        public string MainStreet { get; set; }
        public string OtherStreet { get; set; }
        public string PostalCode { get; set; }
        public Nullable<byte> IsDefault { get; set; }
        public Nullable<int> DefauleStoreId { get; set; }
        public Nullable<long> Lat { get; set; }
        public Nullable<long> Lng { get; set; }
        //public Nullable<Guid> CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
