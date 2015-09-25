using Aantoli.Framework.Entity.Base;
using Aantoli.Common.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aantoli.Common.Entity.Gateway.Customer
{
    public class CustomerEntity : ShipAddressEntity
    {
        public SOURCE_TYPE RequestSourceId { get; set; }
        public string DeviceIMEI { get; set; }
        public int CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string BirthPDate { get; set; }
        public DateTime BirthDate { get; set; }
        public ShipAddressListEntity ShippingAddressList { get; set; }
    }
}
