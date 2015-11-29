namespace Anatoli.DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Customer : BaseModel
    {
        //public Guid CustomerUniqueId { get; set; }
        public Nullable<long> CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public Nullable<DateTime> BirthDay { get; set; }
        public string Phone { get; set; }
        public Nullable<long> Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> ZoneId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> CustomerMainAppId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<long> ActionSourceValueId { get; set; }
        public string DeviceIMEI { get; set; }
        //public Guid CustomerId { get; set; }
    
        public virtual ICollection<CustomerShipAddress> CustomerShipAddresses { get; set; }
    }
}
