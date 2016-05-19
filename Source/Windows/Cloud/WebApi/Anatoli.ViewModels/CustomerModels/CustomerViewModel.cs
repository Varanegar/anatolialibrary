using Anatoli.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.ViewModels.CustomerModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public long? CustomerCode { get; set; }
        public string CustomerName
        {
            get
            {
                return FirstName + " " + LastName ;
            }
            set { }
        }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string MainStreet { get; set; }
        public string OtherStreet { get; set; }
        public string PostalCode { get; set; }
        public string NationalCode { get; set; }
        public Nullable<Guid> RegionInfoId { get; set; }
        public Nullable<Guid> RegionLevel1Id { get; set; }
        public Nullable<Guid> RegionLevel2Id { get; set; }
        public Nullable<Guid> RegionLevel3Id { get; set; }
        public Nullable<Guid> RegionLevel4Id { get; set; }
        public Nullable<Guid> DefauleStoreId { get; set; }
        public Guid CompanyId { get; set; }

        public List<CustomerShipAddressViewModel> CustomerShipAddress { get; set; }
    }
}
