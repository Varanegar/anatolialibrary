using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.RequestModel
{
    public class CustomerRequestModel : BaseRequestModel
    {
        public string regionId { get; set; }
        public string customerId { get; set; }
        public string basketId { get; set; }
        //public CustomerShipAddressViewModel customerShipAddressData { get; set; }
        //public CustomerViewModel customerData { get; set; }
        //public List<BasketItemViewModel> basketItemData { get; set; }
        //public List<BasketViewModel> basketData { get; set; }
        //public List<CustomerViewModel> customerListData { get; set; }
    }
}
