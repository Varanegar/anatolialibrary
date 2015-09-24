using Aantoli.Common.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Customer
{
    public class BaseAddressEntity : BaseEntity
    {
        public string AddressName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public Guid ZoneId { get; set; }
        public string MainStreet { get; set; }
        public string OtherStreet { get; set; }
        public string PostalCode { get; set; }
    }
}
