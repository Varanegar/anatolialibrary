using System;

namespace Anatoli.ViewModels.CustomerModels
{
    public class CustomerShipAddressViewModel : BaseViewModel
    {
        public string AddressName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string MainStreet { get; set; }
        public string OtherStreet { get; set; }
        public string PostalCode { get; set; }
        public string Transferee { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public Guid? RegionInfoId { get; set; }
        public Guid? RegionLevel1Id { get; set; }
        public Guid? RegionLevel2Id { get; set; }
        public Guid? RegionLevel3Id { get; set; }
        public Guid? RegionLevel4Id { get; set; }
        public Guid? DefauleStoreId { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
